/*
    MySQL ストアドファンクション

        https://qiita.com/dskst/items/38838da26645ebe7bfe8
        
        MySQLデータベースサーバーのターミナルにSQL文を貼り付けて(要SJIS)ストアドプロシージャとして登録

        ターミナルでのログイン・ログアウト方法
            > mysql -u root -pKoken4151@ -P 53306 koken_5
            > \q
*/

DROP FUNCTION IF EXISTS f_han2zen;
DELIMITER //
CREATE FUNCTION `f_han2zen`(`str` TEXT) RETURNS TEXT DETERMINISTIC
BEGIN
	-- 半角英語、全角英語
	DECLARE eng_len INT(2);
	DECLARE eng_h VARCHAR(52) DEFAULT 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
	DECLARE eng_z VARCHAR(52) DEFAULT 'ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ';

	-- 半角カナ、全角カナ
	DECLARE kana1_len, kana2_len INT(2);
	DECLARE kana1_h VARCHAR(61) DEFAULT 'ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝｯｬｭｮｧｨｩｪｫｰ｡｢｣､･';
	DECLARE kana1_z VARCHAR(61) DEFAULT 'アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲンッャュョァィゥェォー。「」、・';
	DECLARE kana2_h VARCHAR(52) DEFAULT 'ｶﾞｷﾞｸﾞｹﾞｺﾞｻﾞｼﾞｽﾞｾﾞｿﾞﾀﾞﾁﾞﾂﾞﾃﾞﾄﾞﾊﾞﾋﾞﾌﾞﾍﾞﾎﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟｳﾞ';
	DECLARE kana2_z VARCHAR(52) DEFAULT 'ガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポヴ';

	-- 半角数字、全角数字
	DECLARE int_len INT(2);
	DECLARE int_h VARCHAR(10) DEFAULT '1234567890';
	DECLARE int_z VARCHAR(10) DEFAULT '１２３４５６７８９０';

	 -- 半角記号、全角記号（;/=*を追加）
	DECLARE symbol_len INT (2);
	DECLARE symbol_h VARCHAR(14) DEFAULT '---,.:;()&;/=*';
	DECLARE symbol_z VARCHAR(14) DEFAULT 'ー―－，．：；（）＆；／＝＊';

	-- 半角英語から全角英語に変換
	SET eng_len = CHAR_LENGTH(eng_h);
	WHILE eng_len > 0 DO
		SET str = REPLACE(str, SUBSTRING(eng_h, eng_len, 1), SUBSTRING(eng_z, eng_len, 1));
		SET eng_len = eng_len - 1;
	END WHILE;

	-- 半角カナから全角カナに変換
	SET kana2_len = CHAR_LENGTH(kana2_z);
	SET kana1_len = CHAR_LENGTH(kana1_z);
	WHILE kana2_len > 0 DO
		SET str = REPLACE(str, SUBSTRING(kana2_h, kana2_len*2-1, 2), SUBSTRING(kana2_z, kana2_len, 1));
		SET kana2_len = kana2_len - 1;
	END WHILE;
	WHILE kana1_len > 0 DO
		SET str = REPLACE(str, SUBSTRING(kana1_h, kana1_len, 1), SUBSTRING(kana1_z, kana1_len, 1));
		SET kana1_len = kana1_len - 1;
	END WHILE;

	-- 半角数字から全角数字に変換
	SET int_len = CHAR_LENGTH(int_z);
	WHILE int_len > 0 DO
		SET str = REPLACE(str, SUBSTRING(int_h, int_len, 1), SUBSTRING(int_z, int_len, 1));
		SET int_len = int_len - 1;
	END WHILE;

	-- 半角ハイフン系を全角ハイフンに変換
	SET symbol_len = CHAR_LENGTH(symbol_z);
	WHILE symbol_len > 0 DO
		SET str = REPLACE(str, SUBSTRING(symbol_h, symbol_len, 1), SUBSTRING(symbol_z, symbol_len, 1));
		SET symbol_len = symbol_len - 1;
	END WHILE;

	-- 半角スペースを全角スペースに変換
	SET str = REPLACE(str, ' ', '　');

	RETURN str;
END
//
DELIMITER ;
