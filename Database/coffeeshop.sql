-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 07 Bulan Mei 2025 pada 11.50
-- Versi server: 10.4.32-MariaDB
-- Versi PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `coffeeshop`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `data`
--

CREATE TABLE `data` (
  `id` int(25) NOT NULL,
  `nama` varchar(30) NOT NULL,
  `tanggal` date NOT NULL,
  `jumlah_beli` varchar(30) NOT NULL,
  `total_biaya` varchar(30) NOT NULL,
  `bayar` varchar(30) NOT NULL,
  `kembali` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `data`
--

INSERT INTO `data` (`id`, `nama`, `tanggal`, `jumlah_beli`, `total_biaya`, `bayar`, `kembali`) VALUES
(10, 'Rojali', '2024-12-05', '1', '15.000', '20000', '5.000'),
(12, 'Erik', '2024-12-05', '3,', '45.000', '50000', '5.000'),
(13, 'Olip', '2024-12-05', '1,', '20.000', '20000', '0'),
(14, 'Shabrina', '2024-12-05', '1,1,', '40.000', '50000', '10.000'),
(15, 'Lenny', '2024-12-05', '1,', '15.000', '20000', '5.000'),
(16, 'Aden', '2024-12-05', '1,', '15.000', '15000', '0'),
(17, 'Bandi', '2024-12-12', '1,', '15.000', '20000', '5.000'),
(18, 'Dhani', '2024-12-12', '1,', '30.000', '30000', '0'),
(19, 'Ferdi', '2024-12-12', '1,', '20.000', '20000', '0'),
(20, 'Reza', '2024-12-12', '1,', '15.000', '20000', '5.000'),
(21, 'Bandi', '2024-12-30', '1,', '15.000', '20000', '5.000'),
(22, 'Bima', '2025-01-14', '1,', '20.000', '50000', '30.000');

-- --------------------------------------------------------

--
-- Struktur dari tabel `data_menu`
--

CREATE TABLE `data_menu` (
  `id_data` int(25) NOT NULL,
  `id_menu` int(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `data_menu`
--

INSERT INTO `data_menu` (`id_data`, `id_menu`) VALUES
(10, 1),
(12, 4),
(13, 5),
(14, 3),
(14, 5),
(15, 2),
(16, 2),
(17, 7),
(18, 8),
(19, 5),
(20, 2),
(21, 1),
(22, 5);

-- --------------------------------------------------------

--
-- Struktur dari tabel `menu`
--

CREATE TABLE `menu` (
  `id` int(25) NOT NULL,
  `menu` varchar(25) NOT NULL,
  `harga` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `menu`
--

INSERT INTO `menu` (`id`, `menu`, `harga`) VALUES
(1, 'Americano', '15000'),
(2, 'Espresso', '15000'),
(3, 'Caramel Machiatto', '20000'),
(4, 'Cappucino', '15000'),
(5, 'Caramel Latte', '20000'),
(7, 'Torabica', '15000'),
(8, 'Cookies n Cream Coffee', '30000'),
(9, 'Mocha Coffee', '20000');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `data`
--
ALTER TABLE `data`
  ADD PRIMARY KEY (`id`);

--
-- Indeks untuk tabel `data_menu`
--
ALTER TABLE `data_menu`
  ADD UNIQUE KEY `id_data` (`id_data`,`id_menu`),
  ADD UNIQUE KEY `id_data_2` (`id_data`,`id_menu`),
  ADD UNIQUE KEY `id_data_3` (`id_data`,`id_menu`),
  ADD UNIQUE KEY `id_data_4` (`id_data`,`id_menu`),
  ADD KEY `fk_menu` (`id_menu`);

--
-- Indeks untuk tabel `menu`
--
ALTER TABLE `menu`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `data`
--
ALTER TABLE `data`
  MODIFY `id` int(25) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `data_menu`
--
ALTER TABLE `data_menu`
  ADD CONSTRAINT `fk_data` FOREIGN KEY (`id_data`) REFERENCES `data` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_menu` FOREIGN KEY (`id_menu`) REFERENCES `menu` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
