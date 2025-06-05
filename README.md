# Sistem Parkir Konsol (.NET)

## Overview

Implementasi sistem parkir berbasis konsol menggunakan C# .NET, sesuai dengan spesifikasi yang diberikan. Sistem ini mengelola lot parkir untuk mobil kecil dan motor, mencatat kendaraan, dan menyediakan berbagai laporan.

## Fitur

### Manajemen Lot Parkir

- Membuat parking lot dengan jumlah slot tertentu.
- Setiap slot dapat menampung 1 mobil atau 1 motor.

### Check-In Kendaraan:

- Mengalokasikan slot yang tersedia secara sekuensial.
- Hanya menerima "Mobil" dan "Motor".
- Mencatat Nomor Polisi, Warna, dan Jenis Kendaraan.
- Perhitungan biaya parkir per jam (meskipun tidak di-output, waktu check-in dicatat).

### Check-Out Kendaraan:

- Mengosongkan slot agar tersedia kembali.

### Laporan (Report):

- Jumlah lot yang terisi dan tersedia.
- Status parking lot (daftar kendaraan di setiap slot).
- Jumlah kendaraan berdasarkan jenis (Mobil/Motor).
- Nomor registrasi kendaraan berdasarkan plat ganjil/genap.
- Nomor registrasi kendaraan berdasarkan warna.
- Nomor slot kendaraan berdasarkan warna.
- Nomor slot berdasarkan nomor registrasi.

## Cara Menjalankan Aplikasi

Aplikasi ini dibangun menggunakan C# .NET 5 (atau versi .NET Core/5+ lainnya). Untuk menjalankannya, Anda memerlukan .NET SDK yang terinstal di sistem Anda.

### 1. Pastikan .NET SDK Terinstal

Anda bisa mengunduh dan menginstal .NET SDK dari situs resmi Microsoft:

```
https://dotnet.microsoft.com/download
```

### 2. Kloning Repositori dari GitHub

- Buka Terminal atau Command Prompt di komputer kamu.
- Navigasi ke direktori di mana kamu ingin menyimpan proyek ini (misalnya D:\Projects).
- Gunakan perintah `git clone`

```
https://github.com/anastasyawlf/parking-system
```

### 3. Navigasi ke Direktori Proyek C#

Masuk ke dalam folder tersebut, lalu masuk lagi ke folder SystemParkingApp (yang berisi Program.cs dan .csproj).

```
cd system-parking/SystemParkingApp
```

### 4. Jalankan Aplikasi:

Untuk menjalankan aplikasi, gunakan perintah berikut:

```
dotnet run
```

### 5. Contoh Penggunaan

Anda dapat berinteraksi dengan sistem dengan mengetikkan perintah-perintah berikut setelah prompt $:

```
$ create_parking_lot 6
Created a parking lot with 6 slots
$ park B-1234-XYZ Putih Mobil
Allocated slot number: 1
$ park B-9999-XYZ Putih Motor
Allocated slot number: 2
$ park D-0001-HIJ Hitam Mobil
Allocated slot number: 3
$ park B-7777-DEF Merah Mobil
Allocated slot number: 4
$ park B-2701-XXX Biru Mobil
Allocated slot number: 5
$ park B-3141-ZZZ Hitam Motor
Allocated slot number: 6
$ leave 4
Slot number 4 is free
$ status
Slot No.    Type        Registration No     Colour
1           Mobil       B-1234-XYZ          Putih
2           Motor       B-9999-XYZ          Putih
3           Mobil       D-0001-HIJ          Hitam
5           Mobil       B-2701-XXX          Biru
6           Motor       B-3141-ZZZ          Hitam
$ park B-333-SSS Putih Mobil
Allocated slot number: 4
$ park A-1212-GGG Putih Mobil
Sorry, parking lot is full
$ type_of_vehicles Motor
2
$ type_of_vehicles Mobil
4
$ registration_numbers_for_vehicles_with_ood_plate
B-9999-XYZ, D-0001-HIJ, B-2701-XXX
$ registration_numbers_for_vehicles_with_event_plate
B-1234-XYZ, B-3141-ZZZ, B-333-SSS
$ registration_numbers_for_vehicles_with_colour Putih
B-1234-XYZ, B-9999-XYZ, B-333-SSS
$ slot_numbers_for_vehicles_with_colour Putih
1, 2, 4
$ slot_number_for_registration_number B-3141-ZZZ
6
$ slot_number_for_registration_number Z-1111-AAA
Not found
$ occupied_slots_count
Jumlah lot terisi: 6
$ available_slots_count
Jumlah lot tersedia: 0
$ exit
```
