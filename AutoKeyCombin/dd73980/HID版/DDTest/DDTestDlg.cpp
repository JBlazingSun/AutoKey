
// DDTestDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "DDTest.h"
#include "DDTestDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CDDTestDlg 对话框



CDDTestDlg::CDDTestDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CDDTestDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CDDTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CDDTestDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_find, &CDDTestDlg::OnBnClickedButtonfind)
	ON_BN_CLICKED(IDC_BUTTON1, &CDDTestDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CDDTestDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &CDDTestDlg::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON4, &CDDTestDlg::OnBnClickedButton4)
	ON_BN_CLICKED(IDC_BUTTON5, &CDDTestDlg::OnBnClickedButton5)
	ON_BN_CLICKED(IDC_BUTTON6, &CDDTestDlg::OnBnClickedButton6)
	ON_BN_CLICKED(IDC_BUTTON7, &CDDTestDlg::OnBnClickedButton7)
	ON_BN_CLICKED(IDC_BUTTON8, &CDDTestDlg::OnBnClickedButton8)

	ON_WM_COPYDATA()
	ON_WM_TIMER()
	ON_WM_HOTKEY()
END_MESSAGE_MAP()

void CDDTestDlg::SetHotKey(void)
{
	::RegisterHotKey(GetSafeHwnd(),6689,                0           ,  VK_F3 ); 
	::RegisterHotKey(GetSafeHwnd(),6690,                0           ,  VK_F4 ); 

}

// CDDTestDlg 消息处理程序

BOOL CDDTestDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码
	SetHotKey();
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CDDTestDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作区矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CDDTestDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


//加载DDHID.dll
void CDDTestDlg::OnBnClickedButtonfind()
{
	CFileDialog 	dlg(TRUE,L"DD",L"", OFN_HIDEREADONLY  , L"DD入口文件|*.dll" ,NULL,0,TRUE);

	WCHAR fileBuffer[MAX_PATH] = {0};
	dlg.m_ofn.lpstrFile = fileBuffer;
	dlg.m_ofn.nMaxFile= MAX_PATH;

	if (  dlg.DoModal() != IDOK)
	{
		return;
	}

	CString dllfile = dlg.GetPathName();
	(GetDlgItem(IDC_EDIT1))->SetWindowTextW(dllfile);
	int ret = dd.GetFunAddr(dllfile) ;

	if ( ret == 1 )
	{
		//DLL加载成功
		int r = dd.DD_int(0);
		if (1==r)
		{
			//返回1，初始化成功
			(GetDlgItem(IDC_BUTTON1))->EnableWindow(TRUE);
			(GetDlgItem(IDC_BUTTON2))->EnableWindow(TRUE);
			(GetDlgItem(IDC_BUTTON3))->EnableWindow(TRUE);
			(GetDlgItem(IDC_BUTTON4))->EnableWindow(TRUE);
			(GetDlgItem(IDC_BUTTON5))->EnableWindow(TRUE);
			(GetDlgItem(IDC_BUTTON6))->EnableWindow(TRUE);	
			(GetDlgItem(IDC_BUTTON7))->EnableWindow(TRUE);	
			//(GetDlgItem(IDC_BUTTON8))->EnableWindow(TRUE);		
		}
		else
		{
			//初始化错误
			if (r==-4)
			{
				AfxMessageBox(L"网络不通");
			} 
			else if(r==-7)
			{
				AfxMessageBox(L"网络验证错误,稍后再试或检查更新");
			}
			else
			{
				CString s;
				s.Format(L"初始化错误: %d ", r);
				AfxMessageBox(s);
			}
		}
	}
}

//键盘单键
void CDDTestDlg::OnBnClickedButton1()
{
	dd.DD_key(601 ,1);
	dd.DD_key(601 ,2);

}

//相对移动
void CDDTestDlg::OnBnClickedButton2()
{
	dd.DD_movR(100,100);
	//两个参数dx,dy .  -127 < 取值范围 < 127 

}

//鼠标左右键
void CDDTestDlg::OnBnClickedButton3()
{
	AfxMessageBox(L"==");
	Sleep(5000);

	dd.DD_btn(1);	dd.DD_btn(2);
	Sleep(1000);
	dd.DD_btn(4);	dd.DD_btn(8);

}

//滚轮
void CDDTestDlg::OnBnClickedButton4()
{
	AfxMessageBox(L"==");
	Sleep(5000);

	dd.DD_whl(10);
	Sleep(1000);
	dd.DD_whl(-10);

}

//键盘可见字符串
void CDDTestDlg::OnBnClickedButton5()
{
	// TODO: 在此添加控件通知处理程序代码
	AfxMessageBox(L"==");
	Sleep(5000);

	dd.DD_str("123ABCefg@dd");

	return;
}


BOOL CDDTestDlg::OnCopyData(CWnd* pWnd, COPYDATASTRUCT* pCopyDataStruct)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	int len = pCopyDataStruct->cbData ;
	PCHAR ss = (PCHAR)pCopyDataStruct->lpData;
	char buf[255]={0};
	strcpy_s(buf, ss);

	dd.DD_str(buf);

	return CDialogEx::OnCopyData(pWnd, pCopyDataStruct);
}

//组合键
void CDDTestDlg::OnBnClickedButton6()
{
	//ctrl+alt+del
	dd.DD_key(600,1);	
	dd.DD_key(602,1);	
	dd.DD_key(706,1);	
	dd.DD_key(706,2);
	dd.DD_key(602,2);
	dd.DD_key(600,2);
}

//绝对移动
void CDDTestDlg::OnBnClickedButton7()
{
	dd.DD_mov(500,500);
	//dd.DD_mov(0,0);
}

//其它
void CDDTestDlg::OnBnClickedButton8()
{
	AfxMessageBox(L"==");
	Sleep(5000);
	//SetTimer(1,10,0);return;//自动

	//拖动
	dd.DD_mov(300,300);Sleep(500);
	dd.DD_btn(1);	Sleep(100);
	dd.DD_mov(100,100);Sleep(3000);
	dd.DD_btn(2);
}


void CDDTestDlg::OnTimer(UINT_PTR nIDEvent)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	switch(nIDEvent)
	{
	case 1:
		dd.DD_mov(1,0);
	}
	CDialogEx::OnTimer(nIDEvent);
}


void CDDTestDlg::OnHotKey(UINT nHotKeyId, UINT nKey1, UINT nKey2)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	switch (nHotKeyId)
	{
	case  6689:  // 模拟输入键盘上可见字符
		dd.DD_mov(10,00);
		break;
	case 6690:
		dd.DD_mov(-10,0);
	}
	CDialogEx::OnHotKey(nHotKeyId, nKey1, nKey2);
}

