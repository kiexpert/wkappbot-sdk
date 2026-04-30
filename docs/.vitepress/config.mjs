import { defineConfig } from 'vitepress'

export default defineConfig({
  title: 'WKAppBot',
  description: 'Windows + Android UI Automation for AI Agents. Focusless. Self-healing. AI-native.',
  base: '/wkappbot-sdk/',
  lang: 'ko',
  ignoreDeadLinks: true,

  head: [
    ['link', { rel: 'icon', href: '/wkappbot-sdk/favicon.ico' }],
    ['meta', { name: 'theme-color', content: '#f55' }],
  ],

  themeConfig: {
    logo: '/logo.png',
    siteTitle: 'WKAppBot',

    nav: [
      { text: '가이드', link: '/guide/install' },
      { text: '레퍼런스', link: '/reference/commands' },
      { text: '고급', link: '/advanced/architecture' },
      { text: '요금', link: '/pricing' },
      {
        text: 'v6.0.0',
        items: [
          { text: 'Changelog', link: 'https://github.com/kiexpert/wkappbot-sdk/releases' },
          { text: 'GitHub', link: 'https://github.com/kiexpert/wkappbot-sdk' },
        ],
      },
    ],

    sidebar: {
      '/guide/': [
        {
          text: '시작하기',
          items: [
            { text: '설치', link: '/guide/install' },
            { text: '빠른 시작 (10분)', link: '/guide/quickstart' },
            { text: '핵심 개념', link: '/guide/concepts' },
          ],
        },
        {
          text: '도움말',
          items: [
            { text: 'FAQ', link: '/faq' },
            { text: '트러블슈팅', link: '/guide/troubleshooting' },
          ],
        },
      ],
      '/reference/': [
        {
          text: '레퍼런스',
          items: [
            { text: 'CLI 명령어 전체', link: '/reference/commands' },
            { text: 'grap 패턴', link: '/reference/grap' },
            { text: 'YAML 시나리오', link: '/reference/scenarios' },
          ],
        },
      ],
      '/advanced/': [
        {
          text: '고급',
          items: [
            { text: '아키텍처', link: '/advanced/architecture' },
            { text: '라이선스 시스템', link: '/advanced/licensing' },
          ],
        },
      ],
    },

    socialLinks: [
      { icon: 'github', link: 'https://github.com/kiexpert/wkappbot-sdk' },
    ],

    footer: {
      message: 'Launcher: MIT License. Core binary: proprietary.',
      copyright: 'Copyright © 2024–2026 kivilab.co.kr',
    },

    search: {
      provider: 'local',
    },

    editLink: {
      pattern: 'https://github.com/kiexpert/wkappbot-sdk/edit/main/docs/:path',
      text: 'GitHub에서 수정',
    },

    lastUpdated: {
      text: '최종 업데이트',
    },
  },
})
