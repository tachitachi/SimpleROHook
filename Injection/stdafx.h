// stdafx.h : �W���̃V�X�e�� �C���N���[�h �t�@�C���̃C���N���[�h �t�@�C���A�܂���
// �Q�Ɖ񐔂������A�����܂�ύX����Ȃ��A�v���W�F�N�g��p�̃C���N���[�h �t�@�C��
// ���L�q���܂��B
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Windows �w�b�_�[����g�p����Ă��Ȃ����������O���܂��B
// Windows �w�b�_�[ �t�@�C��:
#include <windows.h>
#include <WinSock2.h>
#include <Shlwapi.h>
#include <mmsystem.h>
#include <Richedit.h>
#include <tchar.h>

#pragma comment(lib, "shlwapi.lib")
#pragma comment(lib, "ddraw.lib")
#pragma comment(lib, "dinput8.lib")
#pragma comment(lib, "dxguid.lib")
#pragma comment(lib, "winmm.lib")


#define DIRECTINPUT_VERSION  0x0700
#include <ddraw.h>
#include <d3d.h>
#include <dinput.h>

// TODO: �v���O�����ɕK�v�Ȓǉ��w�b�_�[�������ŎQ�Ƃ��Ă��������B
#include <process.h>

#include <math.h>

#include <vector>
#include <array>
#include <list>
#include <map>

#include <iostream>
#include <iomanip>

#include <string>
#include <sstream>
#include <fstream>

#include <cwchar>

#include "MinHook.h"