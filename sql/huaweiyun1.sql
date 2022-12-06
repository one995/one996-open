-- --------------------------------------------------------
-- 主机:                           127.0.0.1
-- 服务器版本:                        8.0.28 - MySQL Community Server - GPL
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 导出 like 的数据库结构
CREATE DATABASE IF NOT EXISTS `like` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `like`;

-- 导出  表 like.artclelike 结构
CREATE TABLE IF NOT EXISTS `artclelike` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `userid` bigint NOT NULL COMMENT '点赞人',
  `artcleid` bigint NOT NULL COMMENT '被点赞的文章',
  `likeconut` int NOT NULL DEFAULT '0' COMMENT '点赞次数与',
  `createtime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci COMMENT='点赞表';

-- 正在导出表  like.artclelike 的数据：~0 rows (大约)
DELETE FROM `artclelike`;
/*!40000 ALTER TABLE `artclelike` DISABLE KEYS */;
/*!40000 ALTER TABLE `artclelike` ENABLE KEYS */;

-- 导出  表 like.article 结构
CREATE TABLE IF NOT EXISTS `article` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `title` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '0',
  `artcletext` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '文章内容',
  `userid` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT '文章发表人',
  `createtime` datetime NOT NULL,
  `msgcount` int NOT NULL DEFAULT '0' COMMENT '文章被评论次数',
  `isdelete` int DEFAULT NULL,
  `AID` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateBy` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci COMMENT='文章表';

-- 正在导出表  like.article 的数据：~2 rows (大约)
DELETE FROM `article`;
/*!40000 ALTER TABLE `article` DISABLE KEYS */;
INSERT INTO `article` (`id`, `title`, `artcletext`, `userid`, `createtime`, `msgcount`, `isdelete`, `AID`, `UpdateTime`, `UpdateBy`) VALUES
	(3, '22', 'string', '11', '0001-01-01 00:00:00', 0, 1, NULL, '2022-02-03 00:40:52', '11'),
	(4, 'string', 'string', 'string', '2022-02-02 16:49:20', 0, 0, '90a9ec9435c4453fa4f505b6c3b8b4c7', '0001-01-01 00:00:00', NULL);
/*!40000 ALTER TABLE `article` ENABLE KEYS */;

-- 导出  表 like.collection 结构
CREATE TABLE IF NOT EXISTS `collection` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `userid` bigint NOT NULL DEFAULT '0',
  `artcleid` bigint NOT NULL DEFAULT '0',
  `collectionconut` bigint NOT NULL DEFAULT '0',
  `createtime` datetime NOT NULL,
  `touserid` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci COMMENT='收藏表';

-- 正在导出表  like.collection 的数据：~0 rows (大约)
DELETE FROM `collection`;
/*!40000 ALTER TABLE `collection` DISABLE KEYS */;
/*!40000 ALTER TABLE `collection` ENABLE KEYS */;

-- 导出  表 like.comments 结构
CREATE TABLE IF NOT EXISTS `comments` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `commentsmsg` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '0' COMMENT '被评论内容',
  `createtime` datetime NOT NULL,
  `userid` bigint DEFAULT NULL COMMENT '评论人id',
  `artcleid` bigint DEFAULT NULL COMMENT '被评论人的id',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci COMMENT='评论表';

-- 正在导出表  like.comments 的数据：~0 rows (大约)
DELETE FROM `comments`;
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;

-- 导出  表 like.forwarding 结构
CREATE TABLE IF NOT EXISTS `forwarding` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `userid` bigint NOT NULL DEFAULT '0',
  `artcleid` bigint NOT NULL DEFAULT '0',
  `forwardingconut` bigint NOT NULL DEFAULT '0',
  `createtime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci COMMENT='转发表';

-- 正在导出表  like.forwarding 的数据：~0 rows (大约)
DELETE FROM `forwarding`;
/*!40000 ALTER TABLE `forwarding` DISABLE KEYS */;
/*!40000 ALTER TABLE `forwarding` ENABLE KEYS */;

-- 导出  表 like.loginfo 结构
CREATE TABLE IF NOT EXISTS `loginfo` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `msg` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `level` int NOT NULL DEFAULT '0',
  `cratetime` datetime NOT NULL,
  `localtion` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '0',
  `loginuser` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci COMMENT='日志表';

-- 正在导出表  like.loginfo 的数据：~0 rows (大约)
DELETE FROM `loginfo`;
/*!40000 ALTER TABLE `loginfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `loginfo` ENABLE KEYS */;

-- 导出  表 like.role 结构
CREATE TABLE IF NOT EXISTS `role` (
  `id` int NOT NULL AUTO_INCREMENT,
  `rolelevel` int(10) unsigned zerofill DEFAULT NULL,
  `rolename` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci;

-- 正在导出表  like.role 的数据：~3 rows (大约)
DELETE FROM `role`;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` (`id`, `rolelevel`, `rolename`) VALUES
	(1, 0000000001, '超级管理员'),
	(2, 0000000002, '管理员'),
	(3, 0000000003, '用户');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;

-- 导出  表 like.user 结构
CREATE TABLE IF NOT EXISTS `user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PassWord` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `CreateUser` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PhoneNumber` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '手机号',
  `PersonID` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '账号',
  `NikoID` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '昵称',
  `PicPath` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '头像路径',
  `IDCard` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '身份证号',
  `Role` int DEFAULT NULL COMMENT '用户权限',
  `SEX` int DEFAULT NULL COMMENT '0 女 1 男',
  `Eamil` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '邮箱',
  `VIP` int DEFAULT NULL COMMENT 'vip等级',
  `remake` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '个人介绍',
  `isdelete` int DEFAULT NULL,
  `usertile` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '个人抬头',
  `address` varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '地址',
  `userlevel` int DEFAULT NULL COMMENT '用户等级',
  `nationplace` varchar(20) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '籍贯',
  `focus` int DEFAULT NULL COMMENT '关注人数',
  `fans` int DEFAULT NULL COMMENT '粉丝数',
  `commpany` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '公司',
  `depatment` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '部门',
  `technology` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL COMMENT '技术类型',
  `integralid` int DEFAULT NULL,
  KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci COMMENT='用户表';

-- 正在导出表  like.user 的数据：~3 rows (大约)
DELETE FROM `user`;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`id`, `UserName`, `PassWord`, `CreateTime`, `CreateUser`, `PhoneNumber`, `PersonID`, `NikoID`, `PicPath`, `IDCard`, `Role`, `SEX`, `Eamil`, `VIP`, `remake`, `isdelete`, `usertile`, `address`, `userlevel`, `nationplace`, `focus`, `fans`, `commpany`, `depatment`, `technology`, `integralid`) VALUES
	(1, '111111', '2222222', '2021-11-09 00:03:42', '23325', '4342342', '123456', '345235', '2345234523', '3245234', 2, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
	(4, '742210923@qq.com', '4BFA5111887C94E2ADAF74FA462F7AA5', '2021-11-10 23:24:46', NULL, NULL, '123456789', NULL, NULL, NULL, 1, 0, '742210923@qq.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
	(7, '真的爱你', '1111111111', '2021-12-05 16:54:22', NULL, NULL, 'b374f65983b6c0e', NULL, NULL, NULL, 1, 0, '183320778@qq.com', 0, NULL, 0, NULL, NULL, 0, NULL, 0, 0, NULL, NULL, NULL, 0);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
