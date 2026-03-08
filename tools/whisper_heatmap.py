"""
Whisper Keyboard — Heatmap Analyzer (by Gemini)
Compare normal speech vs whispered speech FFT band distributions.

Usage: python whisper_heatmap.py normal.wav whisper.wav
Output: whisper_comparison.png, normal_energies.csv, whisper_energies.csv
"""
import sys
import numpy as np
import scipy.io.wavfile as wav
import matplotlib.pyplot as plt
import pandas as pd
from scipy.fft import rfft, rfftfreq

def extract_bands(file_path, bands):
    sr, data = wav.read(file_path)
    if len(data.shape) > 1: data = data[:, 0]
    frame_size = int(sr * 0.025)
    hop_size = int(sr * 0.010)
    energies = []

    for i in range(0, len(data) - frame_size, hop_size):
        window = data[i:i+frame_size] * np.hanning(frame_size)
        spectrum = np.abs(rfft(window))
        freqs = rfftfreq(frame_size, 1/sr)

        frame_energies = []
        for low, high in bands:
            idx = np.where((freqs >= low) & (freqs < high))[0]
            val = np.sum(spectrum[idx]) if len(idx) > 0 else 0
            frame_energies.append(val)
        energies.append(frame_energies)

    return np.array(energies)

# 16 Mel-scale bands (matching WhisperCommand.cs)
bands = [
    (80,250), (250,400), (400,550), (550,750), (750,1000), (1000,1300),
    (1300,1700), (1700,2200), (2200,2800), (2800,3500), (3500,4300),
    (4300,5200), (5200,6200), (6200,7500), (7500,9000), (9000,11000),
]

band_names = [
    "Vocal", "F1lo", "F1hi", "F1F2", "F2lo", "F2md", "F2hi", "F2F3",
    "F3", "Sibl", "SibH", "Fric", "Air1", "Air2", "Air3", "Ultra",
]

if len(sys.argv) < 3:
    print("Usage: python whisper_heatmap.py normal.wav whisper.wav")
    sys.exit(1)

normal_wav = sys.argv[1]
whisper_wav = sys.argv[2]

norm_e = extract_bands(normal_wav, bands)
whisp_e = extract_bands(whisper_wav, bands)

# Log scale for visualization
norm_log = np.log1p(norm_e).T
whisp_log = np.log1p(whisp_e).T

fig, (ax1, ax2) = plt.subplots(1, 2, figsize=(15, 6))
ax1.imshow(norm_log, aspect='auto', origin='lower', cmap='magma')
ax1.set_title(f'Normal Speech: {normal_wav}')
ax2.imshow(whisp_log, aspect='auto', origin='lower', cmap='magma')
ax2.set_title(f'Whispered Speech: {whisper_wav}')

for ax in [ax1, ax2]:
    ax.set_ylabel('Mel Band Index')
    ax.set_xlabel('Frame Index')
    ax.set_yticks(range(16))
    ax.set_yticklabels(band_names)

plt.tight_layout()
plt.savefig('whisper_comparison.png', dpi=150)
print(f"Heatmap saved: whisper_comparison.png")

pd.DataFrame(norm_e, columns=band_names).to_csv('normal_energies.csv', index=False)
pd.DataFrame(whisp_e, columns=band_names).to_csv('whisper_energies.csv', index=False)
print(f"CSV saved: normal_energies.csv, whisper_energies.csv")
