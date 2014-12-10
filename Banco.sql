CREATE DATABASE  IF NOT EXISTS `rededb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `rededb`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: localhost    Database: rededb
-- ------------------------------------------------------
-- Server version	5.6.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bandas`
--

DROP TABLE IF EXISTS `bandas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `bandas` (
  `idBanda` bigint(20) NOT NULL AUTO_INCREMENT,
  `nomeBanda` varchar(100) DEFAULT NULL,
  `cidadeNatal` varchar(100) DEFAULT NULL,
  `foto` varchar(255) NOT NULL,
  PRIMARY KEY (`idBanda`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bandas`
--

LOCK TABLES `bandas` WRITE;
/*!40000 ALTER TABLE `bandas` DISABLE KEYS */;
/*!40000 ALTER TABLE `bandas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bandas_usuarios`
--

DROP TABLE IF EXISTS `bandas_usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `bandas_usuarios` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `idUsuario` bigint(20) NOT NULL,
  `idBanda` bigint(20) NOT NULL,
  `lider` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idUsuario` (`idUsuario`),
  KEY `idBanda` (`idBanda`),
  CONSTRAINT `bandas_usuarios_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuarios` (`idUsuario`),
  CONSTRAINT `bandas_usuarios_ibfk_2` FOREIGN KEY (`idBanda`) REFERENCES `bandas` (`idBanda`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bandas_usuarios`
--

LOCK TABLES `bandas_usuarios` WRITE;
/*!40000 ALTER TABLE `bandas_usuarios` DISABLE KEYS */;
/*!40000 ALTER TABLE `bandas_usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `equipamentos`
--

DROP TABLE IF EXISTS `equipamentos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `equipamentos` (
  `idEquip` bigint(20) NOT NULL AUTO_INCREMENT,
  `Usuarios_idUsuario` bigint(20) NOT NULL,
  `descricao` varchar(255) DEFAULT NULL,
  `arquivoFoto` varchar(255) NOT NULL,
  `thumbnail` varchar(255) NOT NULL,
  PRIMARY KEY (`idEquip`,`Usuarios_idUsuario`),
  KEY `Usuarios_idUsuario` (`Usuarios_idUsuario`),
  CONSTRAINT `equipamentos_ibfk_1` FOREIGN KEY (`Usuarios_idUsuario`) REFERENCES `usuarios` (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipamentos`
--

LOCK TABLES `equipamentos` WRITE;
/*!40000 ALTER TABLE `equipamentos` DISABLE KEYS */;
/*!40000 ALTER TABLE `equipamentos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estilos`
--

DROP TABLE IF EXISTS `estilos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `estilos` (
  `idEstilo` bigint(20) NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) NOT NULL,
  PRIMARY KEY (`idEstilo`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estilos`
--

LOCK TABLES `estilos` WRITE;
/*!40000 ALTER TABLE `estilos` DISABLE KEYS */;
INSERT INTO `estilos` VALUES (1,'rock'),(2,'darkwave'),(3,'mpb'),(4,'eletronica');
/*!40000 ALTER TABLE `estilos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `galeria`
--

DROP TABLE IF EXISTS `galeria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `galeria` (
  `idFoto` bigint(20) NOT NULL AUTO_INCREMENT,
  `idBanda` bigint(20) DEFAULT NULL,
  `idUser` bigint(20) DEFAULT NULL,
  `arquivoFoto` varchar(255) NOT NULL,
  `descricao` varchar(255) DEFAULT NULL,
  `thumbnail` varchar(255) NOT NULL,
  PRIMARY KEY (`idFoto`),
  KEY `idUser` (`idUser`),
  KEY `idBanda` (`idBanda`),
  CONSTRAINT `galeria_ibfk_1` FOREIGN KEY (`idUser`) REFERENCES `usuarios` (`idUsuario`),
  CONSTRAINT `galeria_ibfk_2` FOREIGN KEY (`idBanda`) REFERENCES `bandas` (`idBanda`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `galeria`
--

LOCK TABLES `galeria` WRITE;
/*!40000 ALTER TABLE `galeria` DISABLE KEYS */;
/*!40000 ALTER TABLE `galeria` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `musicas`
--

DROP TABLE IF EXISTS `musicas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `musicas` (
  `idMusica` bigint(20) NOT NULL AUTO_INCREMENT,
  `Usuarios_idUsuario` bigint(20) DEFAULT NULL,
  `usuario_nome` varchar(50) DEFAULT NULL,
  `Bandas_idBanda` bigint(20) DEFAULT NULL,
  `nomeMusica` varchar(100) DEFAULT NULL,
  `imagemMusica` varchar(255) DEFAULT NULL,
  `arquivoMusica` varchar(255) NOT NULL,
  `dataPost` date NOT NULL,
  `nOuvida` bigint(20) NOT NULL,
  `Estilos_nome` varchar(55) DEFAULT NULL,
  PRIMARY KEY (`idMusica`),
  KEY `Usuarios_idUsuario` (`Usuarios_idUsuario`),
  KEY `Bandas_idBanda` (`Bandas_idBanda`),
  CONSTRAINT `musicas_ibfk_1` FOREIGN KEY (`Usuarios_idUsuario`) REFERENCES `usuarios` (`idUsuario`),
  CONSTRAINT `musicas_ibfk_2` FOREIGN KEY (`Bandas_idBanda`) REFERENCES `bandas` (`idBanda`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `musicas`
--

LOCK TABLES `musicas` WRITE;
/*!40000 ALTER TABLE `musicas` DISABLE KEYS */;
/*!40000 ALTER TABLE `musicas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `postagens`
--

DROP TABLE IF EXISTS `postagens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `postagens` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `idBanda` bigint(20) DEFAULT NULL,
  `idUser` bigint(20) DEFAULT NULL,
  `arquivoFoto` varchar(255) NOT NULL,
  `texto` text,
  `dataPost` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idUser` (`idUser`),
  KEY `idBanda` (`idBanda`),
  CONSTRAINT `postagens_ibfk_1` FOREIGN KEY (`idUser`) REFERENCES `usuarios` (`idUsuario`),
  CONSTRAINT `postagens_ibfk_2` FOREIGN KEY (`idBanda`) REFERENCES `bandas` (`idBanda`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `postagens`
--

LOCK TABLES `postagens` WRITE;
/*!40000 ALTER TABLE `postagens` DISABLE KEYS */;
/*!40000 ALTER TABLE `postagens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seguindo_seguidores`
--

DROP TABLE IF EXISTS `seguindo_seguidores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `seguindo_seguidores` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `idUser1` bigint(20) NOT NULL,
  `idUser2` bigint(20) DEFAULT NULL,
  `idBanda` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idUser2` (`idUser2`),
  KEY `idUser1` (`idUser1`),
  KEY `idBanda` (`idBanda`),
  CONSTRAINT `seguindo_seguidores_ibfk_2` FOREIGN KEY (`idUser1`) REFERENCES `usuarios` (`idUsuario`),
  CONSTRAINT `seguindo_seguidores_ibfk_3` FOREIGN KEY (`idBanda`) REFERENCES `bandas` (`idBanda`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seguindo_seguidores`
--

LOCK TABLES `seguindo_seguidores` WRITE;
/*!40000 ALTER TABLE `seguindo_seguidores` DISABLE KEYS */;
/*!40000 ALTER TABLE `seguindo_seguidores` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuarios` (
  `idUsuario` bigint(20) NOT NULL AUTO_INCREMENT,
  `nome` varchar(55) NOT NULL,
  `sobrenome` varchar(55) NOT NULL,
  `fotoPerfil` varchar(255) DEFAULT NULL,
  `tipo` varchar(50) DEFAULT NULL,
  `Email` varchar(100) NOT NULL,
  `Senha` varchar(50) NOT NULL,
  PRIMARY KEY (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-12-09 22:49:50
