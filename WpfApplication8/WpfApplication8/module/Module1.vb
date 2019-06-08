Imports System.Runtime.InteropServices
Module Module1
    'Declare Function SendInput Lib "user32.dll" (ByVal nInputs As Long, pInputs As GENERALINPUT, ByVal cbSize As Long) As Long
    'Public Declare Auto Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (Destination As IntPtr, Source As IntPtr, ByVal Length As Long)
    'Public Declare Function SetCursorPos Lib "user32" (ByVal x As Long, ByVal y As Long) As Long
    'Public Declare Function GetWindowRect Lib "user32" (ByVal hWnd As Long, lpRect As RECT) As Long
    'Public Structure RECT
    '    Dim Left As Long
    '    Dim Top As Long
    '    Dim Right As Long
    '    Dim Bottom As Long
    'End Structure
    'Public Structure GENERALINPUT
    '    Public dwType As Long
    '    Public xi() As Byte
    '    'Sub New(ByVal c As Integer)
    '    '    ReDim xi(0 To 23)
    '    'End Sub
    'End Structure
    'Public Structure KEYBDINPUT
    '    Public wVk As Integer
    '    Public wScan As Integer
    '    Public dwFlags As Long
    '    Public time As Long
    '    Public dwExtraInfo As Long
    'End Structure
    'Public Structure MOUSEINPUT
    '    Public dx As Long
    '    Public dy As Long
    '    Public mouseData As Long
    '    Public dwFlags As Long
    '    Public time As Long
    '    Public dwExtraInfo As Long
    'End Structure
    'Const INPUT_KEYBOARD = 1
    'Const INPUT_MOUSE = 0
    'Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Long
    'Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Long, ByVal hWnd2 As Long, ByVal lpsz1 As String, ByVal lpsz2 As String) As Long
    'Public Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As Long) As Long
    'Public Const KEYEVENTF_KEYUP = &H2
    'Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
    ''调用方法举例：
    ''MySendKey vbKeyA '向前台窗口发送字符A
    'Sub MySendKey(bkey As Long)
    '    '参数bkey传入要模拟按键的虚拟码即可模拟按下指定键
    '    Dim GInput(0 To 1) As GENERALINPUT
    '    Dim KInput As KEYBDINPUT = New KEYBDINPUT
    '    KInput.wVk = bkey                   '你要模拟的按键
    '    KInput.dwFlags = 0                  '按下键标志
    '    GInput(0) = New GENERALINPUT()
    '    GInput(0).dwType = INPUT_KEYBOARD
    '    ReDim GInput(0).xi(0 To 24)
    '    GInput(0).xi(0) = New Byte
    '    'CopyMemory(GInput(0).xi(0), structure2ptr(KInput), Len(KInput)) '这个函数用来把内存中KInput的数据复制到GInput
    '    KInput.wVk = bkey
    '    KInput.dwFlags = KEYEVENTF_KEYUP    ' 释放按键
    '    GInput(1) = New GENERALINPUT()
    '    GInput(1).dwType = INPUT_KEYBOARD   ' 表示该消息为键盘消息
    '    ReDim GInput(1).xi(0 To 23)
    '    GInput(1).xi(0) = New Byte
    '    'CopyMemory(GInput(1).xi(0), structure2ptr(KInput), Len(KInput))
    '    '以上工作把按下键和释放键共2条键盘消息加入到GInput数据结构中
    '    SendInput(2, GInput(0), Len(GInput(0)))  '把上面的按键消息插入到windows消息队列
    'End Sub
    'Function structure2ptr(instruct As Object) As IntPtr
    '    Dim ptr = Marshal.AllocHGlobal(Marshal.SizeOf(instruct))
    '    Marshal.StructureToPtr(instruct, ptr, False)
    '    structure2ptr = ptr
    'End Function
    ''调用方法举例：
    ''MySendKey vbKeyA '向前台窗口发送字符A
    'Sub SendSingleKey(bkey As Long)
    '    '参数bkey传入要模拟按键的虚拟码即可模拟按下指定键
    '    Dim GInput As GENERALINPUT = New GENERALINPUT()
    '    Dim KInput As KEYBDINPUT = New KEYBDINPUT()
    '    KInput.wVk = bkey                   '你要模拟的按键
    '    KInput.dwFlags = 0                  '按下键标志
    '    GInput.dwType = INPUT_KEYBOARD
    '    Dim ptr As IntPtr
    '    Marshal.StructureToPtr(KInput, ptr, True)
    '    CopyMemory(GInput.xi(0), ptr, Len(KInput))
    '    SendInput(1, GInput, Len(GInput))  '把上面的按键消息插入到windows消息队列
    'End Sub
    ''调用方法举例：
    'Sub MyMouseClick(x As Long, y As Long)
    '    '移动鼠标
    '    SetCursorPos(x, y)
    '    Dim GInput(0 To 1) As GENERALINPUT
    '    Dim MInput As MOUSEINPUT
    '    MInput.dx = 0
    '    MInput.dy = 0
    '    MInput.mouseData = 0
    '    MInput.dwFlags = &H2
    '    MInput.time = 0
    '    GInput(0).dwType = INPUT_MOUSE
    '    Dim ptr As IntPtr
    '    Marshal.StructureToPtr(MInput, ptr, True)
    '    CopyMemory(GInput(0).xi(0), ptr, Len(MInput)) '这个函数用来把内存中KInput的数据复制到GInput
    '    MInput.dwFlags = &H4
    '    GInput(1).dwType = INPUT_MOUSE
    '    Dim ptr2 As IntPtr
    '    Marshal.StructureToPtr(MInput, ptr2, True)
    '    CopyMemory(GInput(1).xi(0), ptr2, Len(MInput))
    '    '以上工作把按下键和释放键共2条键盘消息加入到GInput数据结构中
    '    SendInput(2, GInput(0), Len(GInput(0))) '把上面的按键消息插入到windows消息队列
    'End Sub
    ''Sub SendClick(sWnd As String, x As Long, y As Long)
    ''    Dim dHwnd As Long, tHwnd As Long, pRec As RECT
    ''    dHwnd = FindWindow(sWnd, vbNullString)
    ''    GetWindowRect(dHwnd, pRec)
    ''    SetCursorPos(pRec.Left + x, pRec.Top + y)
    ''    SetForegroundWindow(dHwnd)
    ''    Dim GInput(0 To 1) As GENERALINPUT
    ''    Dim MInput As MOUSEINPUT
    ''    MInput.dx = 0
    ''    MInput.dy = 0
    ''    MInput.mouseData = 0
    ''    MInput.dwFlags = &H2
    ''    MInput.time = 0
    ''    GInput(0).dwType = 0
    ''    CopyMemory(GInput(0).xi(0), MInput, Len(MInput))
    ''    MInput.dwFlags = &H4
    ''    GInput(1).dwType = 0
    ''    CopyMemory(GInput(1).xi(0), MInput, Len(MInput))
    ''    SendInput(2, GInput(0), Len(GInput(0)))
    ''End Sub
    '''把一个按键消息分成两步，第一步按下，第二步弹起，调用方法举例1：
    '''    MySendKeyStep vbKeyControl, 0'按下
    '''    Sleep 100
    '''    MySendKeyStep vbKeyA, 0'按下
    '''    Sleep 100
    '''    MySendKeyStep vbKeyA, 1'弹起
    '''    Sleep 100
    '''    MySendKeyStep vbKeyControl, 1'弹起
    '''举例2：
    '''    MySendKeyStep vbKeyF3, 0 '按下
    '''    Sleep 100
    '''    MySendKeyStep vbKeyF3, 1 '弹起
    ''Sub MySendKeyStep(bkey As Long, press)
    ''    '参数bkey传入要模拟按键的虚拟码即可模拟按下指定键
    ''    Dim GInput(0 To 1) As GENERALINPUT
    ''    Dim KInput As KEYBDINPUT
    ''    If press = 0 Then                   '如果按键按下
    ''        KInput.wVk = bkey               '你要模拟的按键
    ''        KInput.dwFlags = 0              '按下键标志
    ''        GInput(0).dwType = INPUT_KEYBOARD
    ''        CopyMemory GInput(0).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''        Call SendInput(1, GInput(0), Len(GInput(0))) '把上面的按键消息插入到windows消息队列
    ''    Else                                '如果按键弹起
    ''        KInput.wVk = bkey
    ''        KInput.dwFlags = KEYEVENTF_KEYUP ' 释放按键
    ''        GInput(0).dwType = INPUT_KEYBOARD ' 表示该消息为键盘消息
    ''        CopyMemory GInput(0).xi(0), KInput, Len(KInput)
    ''    Call SendInput(1, GInput(0), Len(GInput(0))) '把上面的按键消息插入到windows消息队列
    ''    End If
    ''End Sub
    '''调用方法举例：
    '''SendKeyCTRLplus vbKeyA 'ctrl+a
    ''Sub SendKeyCTRLplus(bkey As Long)
    ''    '参数bkey传入要模拟按键的虚拟码即可模拟按下指定键
    ''    Dim GInput(0 To 8) As GENERALINPUT
    ''    Dim KInput As KEYBDINPUT
    ''    '按下ctrl
    ''    KInput.wVk = vbKeyControl           '你要模拟的按键
    ''    KInput.dwFlags = 0                  '按下键标志
    ''    GInput(0).dwType = INPUT_KEYBOARD
    ''    CopyMemory GInput(0).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''    Call SendInput(1, GInput(0), Len(GInput(0))) '把上面的按键消息插入到windows消息队列
    ''    Sleep 100
    '''按下bkey
    ''    KInput.wVk = bkey                   '你要模拟的按键
    ''    KInput.dwFlags = 0                  '按下键标志
    ''    GInput(1).dwType = INPUT_KEYBOARD
    ''    CopyMemory GInput(1).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''    Call SendInput(1, GInput(1), Len(GInput(1))) '把上面的按键消息插入到windows消息队列
    ''    Sleep 100
    '''弹起bkey
    ''    KInput.wVk = bkey
    ''    KInput.dwFlags = KEYEVENTF_KEYUP    ' 释放按键
    ''    GInput(2).dwType = INPUT_KEYBOARD   ' 表示该消息为键盘消息
    ''    CopyMemory GInput(2).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''    Call SendInput(1, GInput(2), Len(GInput(2))) '把上面的按键消息插入到windows消息队列
    ''    Sleep 100
    '''弹起ctrl
    ''    KInput.wVk = vbKeyControl
    ''    KInput.dwFlags = KEYEVENTF_KEYUP    ' 释放按键
    ''    GInput(3).dwType = INPUT_KEYBOARD   ' 表示该消息为键盘消息
    ''    CopyMemory GInput(3).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''    Call SendInput(1, GInput(3), Len(GInput(3))) '把上面的按键消息插入到windows消息队列
    ''End Sub
    '''调用方法举例：
    '''SendKeyALTplus vbKeyH, vbKeyA 'ALT+H,A
    ''Sub SendKeyALTplus(bkey As Long, bkey2 As Long)
    ''    '参数bkey传入要模拟按键的虚拟码即可模拟按下指定键
    ''    Dim GInput(0 To 8) As GENERALINPUT
    ''    Dim KInput As KEYBDINPUT
    ''    '按下alt
    ''    KInput.wVk = vbKeyMenu              '你要模拟的按键
    ''    KInput.dwFlags = 0                  '按下键标志
    ''    GInput(0).dwType = INPUT_KEYBOARD
    ''    CopyMemory GInput(0).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''    Call SendInput(1, GInput(0), Len(GInput(0))) '把上面的按键消息插入到windows消息队列
    ''    Sleep 100
    '''按下bkey
    ''    KInput.wVk = bkey                   '你要模拟的按键
    ''    KInput.dwFlags = 0                  '按下键标志
    ''    GInput(1).dwType = INPUT_KEYBOARD
    ''    CopyMemory GInput(1).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''    Call SendInput(1, GInput(1), Len(GInput(1))) '把上面的按键消息插入到windows消息队列
    ''    Sleep 100
    '''弹起bkey
    ''    KInput.wVk = bkey
    ''    KInput.dwFlags = KEYEVENTF_KEYUP    ' 释放按键
    ''    GInput(2).dwType = INPUT_KEYBOARD   ' 表示该消息为键盘消息
    ''    CopyMemory GInput(2).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''    Call SendInput(1, GInput(2), Len(GInput(2))) '把上面的按键消息插入到windows消息队列
    ''    Sleep 100
    '''弹起alt
    ''    KInput.wVk = vbKeyMenu
    ''    KInput.dwFlags = KEYEVENTF_KEYUP    ' 释放按键
    ''    GInput(3).dwType = INPUT_KEYBOARD   ' 表示该消息为键盘消息
    ''    CopyMemory GInput(3).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''    Call SendInput(1, GInput(3), Len(GInput(3))) '把上面的按键消息插入到windows消息队列
    ''    Sleep 100
    '''按下bkey2
    ''    KInput.wVk = bkey2                  '你要模拟的按键
    ''    KInput.dwFlags = 0                  '按下键标志
    ''    GInput(4).dwType = INPUT_KEYBOARD
    ''    CopyMemory GInput(4).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''    Call SendInput(1, GInput(4), Len(GInput(4))) '把上面的按键消息插入到windows消息队列
    ''    Sleep 100
    '''弹起bkey2
    ''    KInput.wVk = bkey2
    ''    KInput.dwFlags = KEYEVENTF_KEYUP    ' 释放按键
    ''    GInput(5).dwType = INPUT_KEYBOARD   ' 表示该消息为键盘消息
    ''    CopyMemory GInput(5).xi(0), KInput, Len(KInput) '这个函数用来把内存中KInput的数据复制到GInput
    ''    Call SendInput(1, GInput(5), Len(GInput(5))) '把上面的按键消息插入到windows消息队列
    ''End Sub
End Module
