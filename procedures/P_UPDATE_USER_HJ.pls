CREATE OR REPLACE PROCEDURE HMX_KCTC.P_UPDATE_USER_HJ (
    PV_JSON_I       IN  VARCHAR2,
    PV_JSON_O       OUT VARCHAR2,
    PV_RETCOD_O     OUT INTEGER,
    PV_RETMSG_O     OUT VARCHAR2)
/*==============================================================================================================================================
PROCEDURE NAME   : P_UPDATE_USER_HJ
CREATE DATE      : 2024/02/14 (작업중)
ALTER DATE       : 
WRITER           : YHJ
MODIFIER         : 
INPUT PARAMETER  : @PV_JSON_I
OUTPUT PARAMETER : @PV_JSON_O
                   @PV_RETCOD_O - 프로시져 결과 코드
                   @PV_RETMSG_O - 에러 메세지
==============================================================================================================================================*/
IS
    lv_user_id    T_USER.USER_ID%TYPE;
    lv_pwd        T_USER.PWD%TYPE;
    lv_id_t_user  T_USER.ID_T_USER%TYPE;
    lv_nme        T_USER.NME%TYPE;
--    lv_reg_date   T_USER.REG_DATE%TYPE;
--    lv_reg_time   T_USER.REG_TIME%TYPE;
   
   	lv_chg_date	T_USER.CHG_DATE%TYPE;
   	lv_chg_time	T_USER.CHG_TIME%TYPE;
    
    lv_rol_nme    T_ROLE.ROL_NME%TYPE;
   
    lv_id_t_role  T_ROLE.ID_T_ROLE%TYPE; 
   
   	lv_user_exists NUMBER;
   
BEGIN
    -- 0. JSON Parsing
    lv_user_id := JSON_VALUE(PV_JSON_I, '$.USER_ID');
    lv_id_t_user := JSON_VALUE(PV_JSON_I, '$.ID_T_USER');
    lv_pwd := JSON_VALUE(PV_JSON_I, '$.PWD');
    lv_rol_nme := JSON_VALUE(PV_JSON_I, '$.ROL_NME');
    lv_nme := JSON_VALUE(PV_JSON_I, '$.NME');
   
   
    -- 1. 필수값 (USER_ID, PWD, ROL_NME) 검사
	IF lv_user_id IS NULL OR lv_user_id = '' OR lv_pwd IS NULL OR lv_pwd = '' OR lv_rol_nme IS NULL OR lv_rol_nme = '' 
   		THEN
    		PV_RETCOD_O := -1;
    		PV_RETMSG_O := '필수 값(사용자 ID, 패스워드, 계정 유형)을 다시 확인하시기 바랍니다.';
    	RETURN;
	END IF;
    

	-- 2. json에 포함된 모든 값이 유효한 값인지 검사(영한문, 숫자, '-','.',':') 제외 불가
	IF NOT (
    	REGEXP_LIKE(lv_user_id, '^[a-zA-Z0-9가-힣]+$')
    	AND REGEXP_LIKE(lv_rol_nme, '^[a-zA-Z0-9가-힣]+$')
    	AND REGEXP_LIKE(lv_nme, '^[a-zA-Z0-9가-힣]+$')
--    	AND (REGEXP_LIKE(lv_reg_date, '^[a-zA-Z0-9]+$') OR lv_reg_date = '')
--    	AND (REGEXP_LIKE(lv_reg_time, '^[a-zA-Z0-9]+$') OR lv_reg_time = '')
	) THEN
    PV_RETCOD_O := -1;
    PV_RETMSG_O := 'Invalid characters for USER_ID ' || lv_user_id ;
        RETURN;
    END IF;
   
   
    -- 3. 중복 USER_ID 검사
    BEGIN
        SELECT COUNT(*)
        INTO lv_user_exists
        FROM T_USER
        WHERE USER_ID = lv_user_id AND ID_T_USER != lv_id_t_user ; 
        IF lv_user_exists > 0 THEN
            RAISE_APPLICATION_ERROR(-20001, '이미 사용 중인 사용자 ID입니다.');
        END IF;
    EXCEPTION
        WHEN OTHERS THEN
            PV_RETCOD_O := SQLCODE;
            PV_RETMSG_O := SQLERRM;
            RETURN;
    END;
   
   
   -- 4. 비밀번호 유효성 검사
   -- i. 7개의 문자 미만이거나
	IF lv_pwd IS NULL OR lv_pwd = '' THEN
    	PV_RETCOD_O := -1;
    	PV_RETMSG_O := '필수 값(패스워드)가 입력되지 않았습니다.';
    	RETURN;
	END IF;

	-- ii. 영소문자와 숫자 이외의 문자를 포함한 경우
	IF LENGTH(lv_pwd) < 7 OR
   	NOT (REGEXP_LIKE(lv_pwd, '[0-9]') AND REGEXP_LIKE(lv_pwd, '[a-z]')) THEN
    	-- 영문과 숫자를 최소 1개씩 포함하며 7자리 이상의 암호로 변경
    	PV_RETCOD_O := -1;
    	PV_RETMSG_O := '비밀번호는 최소 7자리의 영소문자와 숫자로 구성합니다.';
    	RETURN;
	END IF;
   

    -- 5. 계정 유형 변경 시 상위 권한으로 변경이 불가하도록
   
   
   
    -- 6. T_ROLE 테이블에서 rol_nme(NME) 값에 해당하는 ID_T_ROLE 값을 읽어오기
    BEGIN
        SELECT ID_T_ROLE
        INTO lv_id_t_role
        FROM T_ROLE
        WHERE ROL_NME = lv_rol_nme;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                PV_RETCOD_O := -20002;
                PV_RETMSG_O := '사용할 수 없는 권한입니다.';
                RETURN;
            WHEN OTHERS THEN
                PV_RETCOD_O := SQLCODE;
                PV_RETMSG_O := SQLERRM;
                RETURN;
    END;

	
   -- 7. T_USER 업데이트
	UPDATE T_USER
	SET
    	USER_ID = lv_user_id,
    	PWD = lv_pwd,
    	NME = lv_nme,
    	CHG_DATE = SYSDATE,
    	CHG_TIME = TO_CHAR(SYSDATE, 'HH24:MI:SS'), 
    	ID_T_ROLE = lv_id_t_role
	WHERE ID_T_USER = lv_id_t_user;

    PV_RETCOD_O := 0;
    PV_RETMSG_O := '사용자 정보 갱신 완료';
   
   
END;