##    Log Normalizer

A C# console application for standardizing logs from different formats into a single tabular format.  
It is useful for log analytics, visualization, uploading to databases and monitoring systems.

---

## What does it do

- Recognizes strings ** of two different log formats**
- Converts them into **the same structure**
- Saves the correct lines in `output.txt `
- Problematic lines (with errors or suspicious values) — in `problems.txt `

---

## Example of an input file (`input.txt `)

- `10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'`
- `2025-03-10 15:14:51.5882 | INFO | MobileComputer.GetDeviceId | Код устройства: '@MINDEO-M40-D-410244015546'`
- `2025-03-10 15:14:51.54453 | INFO | M0bileC0mpYter.GetDev1ce1d | Код устройства: '@MINDE0-M42-A-610244015544'`

## Example of the output file (`output.txt `)

- `10.03.2025 15:14:49.523 INFO INFORMATION DEFAULT Версия программы: '3.4.0.48729'`
- `10-03-2025 15:14:51.5882 INFO INFO MobileComputer.GetDeviceId Код устройства: '@MINDEO-M40-D-410244015546'`

## Example of a problematic file (`problems.txt `)

- `2025-03-10 15:14:51.54453 | INFO | M0bileC0mpYter.GetDev1ce1d | Код устройства: '@MINDE0-M42-A-610244015544'`

> Such strings may be invalid in structure or contain suspicious method names (for example, `0` instead of `O`).

---

## Format of output lines

`Дата \t Время \t Уровень \t RawLevel \t Метод \t Сообщение`

---
