Imports System.Runtime.InteropServices

'鼠标按键枚举 
Public Enum MouseBtn
    左下 = 1
    左上 = 2
    右下 = 4
    右上 = 8
End Enum

'组合键枚举
Public Enum KeyModifiers
    None = 0
    Alt = 1
    Control = 2
    Shift = 4
    Windows = 8
End Enum


Public Class CDD

    <DllImport("Kernel32")> Private Shared Function LoadLibrary(ByVal dllfile As String) As System.IntPtr
    End Function
    <DllImport("Kernel32")> Private Shared Function GetProcAddress(ByVal hModule As System.IntPtr, ByVal lpProcName As String) As System.IntPtr
    End Function
    <DllImport("Kernel32")> Private Shared Function FreeLibrary(ByVal hModule As System.IntPtr) As Boolean
    End Function

    Public Delegate Function pDD_btn(ByVal btn As Int32) As Int32
    Public Delegate Function pDD_whl(ByVal whl As Int32) As Int32
    Public Delegate Function pDD_key(ByVal keycode As Int32, ByVal flag As Int32) As Int32
    Public Delegate Function pDD_mov(ByVal x As Int32, ByVal y As Int32) As Int32
    Public Delegate Function pDD_chk() As Int32
    Public Delegate Function pDD_str(ByVal instr As String) As Int32
    Public Delegate Function pDD_todc(ByVal vk As Int32) As Int32
    Public Delegate Function pDD_movR(ByVal dx As Int32, ByVal dy As Int32) As Int32

    Public btn As pDD_btn        ' 鼠标点击 
    Public whl As pDD_whl        ' 鼠标滚动 
    Public mov As pDD_mov     ' 绝对鼠标移动 
    Public movR As pDD_movR     ' 相对鼠标移动 
    Public key As pDD_key        ' 键盘按键 
    Public chk As pDD_chk        ' 驱动检测 
    Public str As pDD_str           ' 键盘字符 
    Public todc As pDD_todc     ' 转换虚拟键码到DD码

    '下面四个函数为增强版功能
    Public Delegate Function pDD_MouseMove(ByVal hwnd As IntPtr, ByVal x As Int32, ByVal y As Int32) As Int32
    Public Delegate Function pDD_SnapPic(ByVal hwnd As IntPtr, ByVal x As Int32, ByVal y As Int32, ByVal w As Int32, ByVal h As Int32) As Int32
    Public Delegate Function pDD_PickColor(ByVal hwnd As IntPtr, ByVal x As Int32, ByVal y As Int32, ByVal mode As Int32) As Int32
    Public Delegate Function pDD_GetActiveWindow() As IntPtr

    Public MouseMove As pDD_MouseMove      '鼠标移动
    Public SnapPic As pDD_SnapPic                    '抓图
    Public PickColor As pDD_PickColor                '取色
    Public GetActiveWindow As pDD_GetActiveWindow  '取激活窗口句柄

    Private m_hinst As IntPtr

    Protected Overrides Sub Finalize()
        Try
            If Not m_hinst.Equals(IntPtr.Zero) Then
                Dim b As Boolean = FreeLibrary(m_hinst)
            End If
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    Public Function Load(ByVal dllfile As String) As Int32
        m_hinst = LoadLibrary(dllfile)

        If m_hinst.Equals(IntPtr.Zero) Then
            Return -2
        Else
            Return GetDDfunAddress(m_hinst)
        End If
    End Function

    '取函数地址返回值  -1：取通用函数地址错误 ，  0：仅取通用函数地址正确 ， 1：取通用函数和增强函数地址都正确
    Private Function GetDDfunAddress(ByVal hinst As IntPtr) As Int32
        Dim ptr As System.IntPtr = IntPtr.Zero

        ptr = GetProcAddress(hinst, "DD_btn")    '鼠标按键
        If ptr.Equals(IntPtr.Zero) Then Return -1
        btn = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_btn)), pDD_btn)

        ptr = GetProcAddress(hinst, "DD_whl")    '鼠标按键
        If ptr.Equals(IntPtr.Zero) Then Return -1
        whl = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_whl)), pDD_whl)

        ptr = GetProcAddress(hinst, "DD_mov")  '鼠标移动
        If ptr.Equals(IntPtr.Zero) Then Return -1
        mov = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_mov)), pDD_mov)

        ptr = GetProcAddress(hinst, "DD_movR")  '鼠标移动
        If ptr.Equals(IntPtr.Zero) Then Return -1
        movR = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_movR)), pDD_movR)

        ptr = GetProcAddress(hinst, "DD_key")    '键盘按键
        If ptr.Equals(IntPtr.Zero) Then Return -1
        key = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_key)), pDD_key)

        'ptr = GetProcAddress(hinst, "DD_chk")    '驱动检测
        'If ptr.Equals(IntPtr.Zero) Then Return -1
        'chk = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_chk)), pDD_chk)

        ptr = GetProcAddress(hinst, "DD_str")       '键盘字符
        If ptr.Equals(IntPtr.Zero) Then Return -1
        str = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_str)), pDD_str)

        ptr = GetProcAddress(hinst, "DD_todc")       '标准虚拟键码转DD 码
        If ptr.Equals(IntPtr.Zero) Then Return -1
        todc = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_todc)), pDD_todc)


        '下面四个函数，只有在增强版中才可用
        ptr = GetProcAddress(hinst, "DD_MouseMove")    '鼠标移动
        If Not ptr.Equals(IntPtr.Zero) Then MouseMove = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_MouseMove)), pDD_MouseMove)

        ptr = GetProcAddress(hinst, "DD_SnapPic")    '抓取图片
        If Not ptr.Equals(IntPtr.Zero) Then SnapPic = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_SnapPic)), pDD_SnapPic)

        ptr = GetProcAddress(hinst, "DD_PickColor")    '取色
        If Not ptr.Equals(IntPtr.Zero) Then PickColor = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_PickColor)), pDD_PickColor)

        ptr = GetProcAddress(hinst, "DD_GetActiveWindow")    '获取激活窗口句柄
        If Not ptr.Equals(IntPtr.Zero) Then GetActiveWindow = TryCast(Marshal.GetDelegateForFunctionPointer(ptr, GetType(pDD_GetActiveWindow)), pDD_GetActiveWindow)

        If MouseMove = Nothing OrElse SnapPic = Nothing OrElse PickColor = Nothing OrElse GetActiveWindow = Nothing Then
            Return 0
        End If

        Return 1

    End Function

End Class
