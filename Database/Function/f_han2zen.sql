/*
    MySQL �X�g�A�h�t�@���N�V����

        https://qiita.com/dskst/items/38838da26645ebe7bfe8
        
        MySQL�f�[�^�x�[�X�T�[�o�[�̃^�[�~�i����SQL����\��t����(�vSJIS)�X�g�A�h�v���V�[�W���Ƃ��ēo�^

        �^�[�~�i���ł̃��O�C���E���O�A�E�g���@
            > mysql -u root -pKoken4151@ -P 53306 koken_5
            > \q
*/

DROP FUNCTION IF EXISTS f_han2zen;
DELIMITER //
CREATE FUNCTION `f_han2zen`(`str` TEXT) RETURNS TEXT DETERMINISTIC
BEGIN
	-- ���p�p��A�S�p�p��
	DECLARE eng_len INT(2);
	DECLARE eng_h VARCHAR(52) DEFAULT 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
	DECLARE eng_z VARCHAR(52) DEFAULT '�����������������������������������������������������`�a�b�c�d�e�f�g�h�i�j�k�l�m�n�o�p�q�r�s�t�u�v�w�x�y';

	-- ���p�J�i�A�S�p�J�i
	DECLARE kana1_len, kana2_len INT(2);
	DECLARE kana1_h VARCHAR(61) DEFAULT '�������������������������������������������ܦݯ��������������';
	DECLARE kana1_z VARCHAR(61) DEFAULT '�A�C�E�G�I�J�L�N�P�R�T�V�X�Z�\�^�`�c�e�g�i�j�k�l�m�n�q�t�w�z�}�~�����������������������������b�������@�B�D�F�H�[�B�u�v�A�E';
	DECLARE kana2_h VARCHAR(52) DEFAULT '�޷޸޹޺޻޼޽޾޿������������������������������߳�';
	DECLARE kana2_z VARCHAR(52) DEFAULT '�K�M�O�Q�S�U�W�Y�[�]�_�a�d�f�h�o�r�u�x�{�p�s�v�y�|��';

	-- ���p�����A�S�p����
	DECLARE int_len INT(2);
	DECLARE int_h VARCHAR(10) DEFAULT '1234567890';
	DECLARE int_z VARCHAR(10) DEFAULT '�P�Q�R�S�T�U�V�W�X�O';

	 -- ���p�L���A�S�p�L���i;/=*��ǉ��j
	DECLARE symbol_len INT (2);
	DECLARE symbol_h VARCHAR(14) DEFAULT '---,.:;()&;/=*';
	DECLARE symbol_z VARCHAR(14) DEFAULT '�[�\�|�C�D�F�G�i�j���G�^����';

	-- ���p�p�ꂩ��S�p�p��ɕϊ�
	SET eng_len = CHAR_LENGTH(eng_h);
	WHILE eng_len > 0 DO
		SET str = REPLACE(str, SUBSTRING(eng_h, eng_len, 1), SUBSTRING(eng_z, eng_len, 1));
		SET eng_len = eng_len - 1;
	END WHILE;

	-- ���p�J�i����S�p�J�i�ɕϊ�
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

	-- ���p��������S�p�����ɕϊ�
	SET int_len = CHAR_LENGTH(int_z);
	WHILE int_len > 0 DO
		SET str = REPLACE(str, SUBSTRING(int_h, int_len, 1), SUBSTRING(int_z, int_len, 1));
		SET int_len = int_len - 1;
	END WHILE;

	-- ���p�n�C�t���n��S�p�n�C�t���ɕϊ�
	SET symbol_len = CHAR_LENGTH(symbol_z);
	WHILE symbol_len > 0 DO
		SET str = REPLACE(str, SUBSTRING(symbol_h, symbol_len, 1), SUBSTRING(symbol_z, symbol_len, 1));
		SET symbol_len = symbol_len - 1;
	END WHILE;

	-- ���p�X�y�[�X��S�p�X�y�[�X�ɕϊ�
	SET str = REPLACE(str, ' ', '�@');

	RETURN str;
END
//
DELIMITER ;
