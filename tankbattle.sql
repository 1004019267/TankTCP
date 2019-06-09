/*
Navicat MySQL Data Transfer

Source Server         : weihaoyu
Source Server Version : 50720
Source Host           : localhost:3306
Source Database       : tankbattle

Target Server Type    : MYSQL
Target Server Version : 50720
File Encoding         : 65001

Date: 2018-10-20 11:38:42
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `account`
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `account` varchar(255) DEFAULT NULL,
  `pwd` varchar(255) DEFAULT NULL,
  `nickname` varchar(255) DEFAULT NULL,
  `diamond` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of account
-- ----------------------------

-- ----------------------------
-- Table structure for `equip`
-- ----------------------------
DROP TABLE IF EXISTS `equip`;
CREATE TABLE `equip` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `account` varchar(255) DEFAULT NULL,
  `tank` int(11) DEFAULT NULL,
  `bullet` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of equip
-- ----------------------------

-- ----------------------------
-- Table structure for `warehouse`
-- ----------------------------
DROP TABLE IF EXISTS `warehouse`;
CREATE TABLE `warehouse` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `account` varchar(255) DEFAULT NULL,
  `itemid` int(11) DEFAULT NULL,
  `count` int(11) DEFAULT NULL,
  `slot` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of warehouse
-- ----------------------------
