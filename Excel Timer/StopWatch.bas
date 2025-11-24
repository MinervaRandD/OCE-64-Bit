Attribute VB_Name = "StopWatch"
Option Explicit

Dim NextTick As Date, t As Date, PreviousTimerValue As Date

Dim timerCell As Range

Public Sub StartTime(ByVal startTimerCell As Range)

    Set timerCell = startTimerCell
    
    PreviousTimerValue = timerCell.Value
    
    t = Time
    
    Call ExcelStopWatch
    
End Sub

Private Sub ExcelStopWatch()
    
    timerCell.Value = Format(Time - t + PreviousTimerValue, "hh:mm:ss")
    
    NextTick = Now + TimeValue("00:00:01")
    
    Application.OnTime NextTick, "ExcelStopWatch"

End Sub

Public Sub StopClock(ByVal stopTimerCell As Range)

    On Error GoTo SubExit
    
    If timerCell <> stopTimerCell Then
    
        Exit Sub
        
    End If
    
    Set timerCell = Nothing
    
    Application.OnTime earliesttime:=NextTick, procedure:="ExcelStopWatch", schedule:=False

SubExit:

End Sub

Public Sub Reset(ByVal resetTimerCell As Range)

    On Error GoTo SubExit
  
    Dim answer As Integer

    answer = MsgBox("Confirm reset timer", vbQuestion + vbYesNo + vbDefaultButton2, "Confirm Reset")

    If answer = 7 Then Exit Sub
    
    resetTimerCell.Value = 0
  
    Application.OnTime earliesttime:=NextTick, procedure:="ExcelStopWatch", schedule:=False
    

SubExit:

End Sub

Private Sub testsub()

Dim answer As Integer

answer = MsgBox("Confirm reset timer", vbQuestion + vbYesCancel + vbDefaultButton2, "Confirm Reset")

End Sub
