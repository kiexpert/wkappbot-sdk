"""wksplit-md: Extract large Pending items from CLAUDE.md into <stem>-ref/ subfolder."""
import re
import sys
import argparse
from pathlib import Path
from datetime import date

NL = chr(10)


def slugify(text, max_len=60):
    text = re.sub(r'[^\w\s-]', '', text.lower())
    text = re.sub(r'[\s_]+', '-', text).strip('-')
    return text[:max_len]


ITEM_HEADER_RE = re.compile(r'^- (\[.\]) \*\*\[([^\]]+)\]\*\* ?(.*)$')
BOUNDARY_RE = re.compile(r'^(- \[.\]|## )')


def parse_pending_items(content):
    lines = content.splitlines(keepends=True)
    pending_start = None
    for i, line in enumerate(lines):
        if re.match(r'^## Pending', line):
            pending_start = i
            break
    if pending_start is None:
        return None, None, []
    items = []
    i = pending_start + 1
    n = len(lines)
    while i < n:
        line = lines[i]
        m = ITEM_HEADER_RE.match(line.rstrip(chr(10)))
        if not m:
            i += 1
            continue
        check, title, first_line_body = m.group(1), m.group(2), m.group(3)
        start_idx = i
        body_lines = [first_line_body] if first_line_body else []
        j = i + 1
        while j < n and not BOUNDARY_RE.match(lines[j]):
            body_lines.append(lines[j].rstrip(chr(10)))
            j += 1
        while body_lines and body_lines[-1] == '':
            body_lines.pop()
        end_idx = j
        full_text = ''.join(lines[start_idx:end_idx])
        items.append({
            'start_idx': start_idx, 'end_idx': end_idx,
            'check': check, 'title': title,
            'body': chr(10).join(body_lines),
            'full_len': len(full_text), 'original': full_text
        })
        i = end_idx
    return lines, pending_start, items


def run(args):
    md_path = Path(args.file)
    if not md_path.exists():
        print('ERROR: ' + str(md_path) + ' not found')
        sys.exit(1)
    content = md_path.read_text(encoding='utf-8')
    lines, pending_start, items = parse_pending_items(content)
    if pending_start is None:
        print('ERROR: No Pending section')
        sys.exit(1)
    out_dir = md_path.parent / (md_path.stem + '-ref')
    today = date.today().isoformat()
    candidates = [it for it in items if it['full_len'] > args.threshold]
    print('items: ' + str(len(items)) + '  over ' + str(args.threshold) + ': ' + str(len(candidates)) + '  -> ' + str(out_dir) + '/')
    if not candidates:
        print('Nothing to extract.')
        return
    total_saved = 0
    new_lines = list(lines)
    for it in sorted(candidates, key=lambda x: x['start_idx'], reverse=True):
        slug = slugify(it['title'])
        out_name = slug + '.md'
        out_path = out_dir / out_name
        rel = out_dir.name + '/' + out_name
        short = it['title'][:40] + ('...' if len(it['title']) > 40 else '')
        snippet = it['body'].split(NL)[0].strip()[:60] if it['body'] else ''
        new_line = '- ' + it['check'] + ' **[' + short + ']** ' + snippet + ' -> [detail](' + rel + ')' + NL
        saved = it['full_len'] - len(new_line)
        total_saved += saved
        pfx = '[DRY] ' if args.dry_run else ''
        print('  ' + pfx + it['title'][:55] + '  (-' + str(saved) + ')')
        if not args.dry_run:
            out_dir.mkdir(exist_ok=True)
            status = 'done' if it['check'] == '[x]' else 'open'
            parts = ['---', NL, 'title: ' + it['title'], NL,
                     'source: ' + md_path.name + ' Pending', NL,
                     'date: ' + today, NL, 'status: ' + status, NL,
                     '---', NL, NL, '# ' + it['title'], NL, NL,
                     it['body'], NL]
            out_path.write_text(''.join(parts), encoding='utf-8')
            new_lines[it['start_idx']:it['end_idx']] = [new_line]
    if not args.dry_run:
        md_path.write_text(''.join(new_lines), encoding='utf-8')
        print('Done: ' + str(len(candidates)) + ' extracted, ~' + str(total_saved) + ' chars saved')
    else:
        print('[DRY-RUN] would extract ' + str(len(candidates)) + ', save ~' + str(total_saved) + ' chars')


def main():
    p = argparse.ArgumentParser()
    p.add_argument('--file', default='CLAUDE.md')
    p.add_argument('--threshold', type=int, default=300)
    p.add_argument('--dry-run', action='store_true')
    run(p.parse_args())


if __name__ == '__main__':
    main()
