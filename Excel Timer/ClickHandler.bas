Attribute VB_Name = "ClickHandler"
Option Explicit

Dim ignoreClick As Boolean

Public Sub Click_Handler(ByVal clickedCell As Range, ByVal altKeyPressed As Boolean)

    Dim stopWatchOffset As Integer
    
    stopWatchOffset = GetStopWatchOffset(clickedCell)
        
    If clickedCell.Count <> 1 Then
    
        Exit Sub
        
    End If
    
    If ignoreClick Then
    
        Exit Sub
        
    End If
    
    If (altKeyPressed) Then
 
        If stopWatchOffset >= 0 Then
        
            Call ClearStopwatchSequence(clickedCell, stopWatchOffset)
            
            Exit Sub
            
        Else
        
            Call SetupTimer(Range(clickedCell(1, 1), clickedCell(1, 4)))
        
            Exit Sub
        
        End If
        
    Else
    
        If stopWatchOffset = 0 Then
        
            Call StartTime(clickedCell(1, 4))
        
            Call ResetCursor(clickedCell)

            Exit Sub
            
        ElseIf stopWatchOffset = 1 Then
        
            Call StopClock(clickedCell(1, 3))
            
            
            Call ResetCursor(clickedCell)

            Exit Sub
        
        ElseIf stopWatchOffset = 2 Then
        
            Call Reset(clickedCell(1, 2))
            
            Call ResetCursor(clickedCell)

            Exit Sub
            
        End If
        
        
    End If
    
    
End Sub

Private Sub ClearStopwatchSequence(ByVal clickedCell As Range, ByVal stopWatchOffset As Integer)

    Dim indx, indxStart, indxStop As Integer
    
    indxStart = 1 - stopWatchOffset
    indxStop = 3 - stopWatchOffset
    
    With Range(clickedCell(1, indxStart), clickedCell(1, indxStop))
    
        .Clear
        
        .ClearContents
        
    End With
     
    Call ResetCursor(clickedCell)
    
End Sub

Private Function GetStopWatchOffset(ByVal clickedCell As Range) As Integer

    If clickedCell(1, 1).Value = "Start" And clickedCell(1, 1).Interior.Color = 5296274 Then
        
        GetStopWatchOffset = 0
        
        Exit Function
        
    End If
    
    If clickedCell(1, 1).Value = "Stop" And clickedCell(1, 1).Interior.Color = 26367 Then
        
        GetStopWatchOffset = 1
        
        Exit Function
        
    End If
    
    If clickedCell(1, 1).Value = "Reset" And clickedCell(1, 1).Interior.Color = 65535 Then
        
        GetStopWatchOffset = 2
        
        Exit Function
        
    End If
    
     
    If clickedCell(1, 1).Interior.Color = 16777164 Then
        
        GetStopWatchOffset = 3
        
        Exit Function
        
    End If
    
    GetStopWatchOffset = -1
    
End Function

Private Sub SetupTimer(ByVal clickedCell As Range)
   
    clickedCell.Clear
    clickedCell.ClearContents
    
    '
    ' Start button
    '
    With clickedCell(1, 1).Interior
        .Pattern = xlSolid
        .PatternColorIndex = xlAutomatic
        .Color = 5296274
        .TintAndShade = 0
        .PatternTintAndShade = 0
    End With
    
    clickedCell(1, 1).FormulaR1C1 = "Start"
    
    '
    ' Stop button
    '
    With clickedCell(1, 2).Interior
        .Pattern = xlSolid
        .PatternColorIndex = xlAutomatic
        .Color = 26367
        .TintAndShade = 0
        .PatternTintAndShade = 0
    End With
    
    clickedCell(1, 2).FormulaR1C1 = "Stop"
    
    '
    ' Reset button
    '
    With clickedCell(1, 3).Interior
        .Pattern = xlSolid
        .PatternColorIndex = xlAutomatic
        .Color = 65535
        .TintAndShade = 0
        .PatternTintAndShade = 0
    End With
    
    clickedCell(1, 3).FormulaR1C1 = "Reset"
    
    '
    ' Value cell
    '
    With clickedCell
        .HorizontalAlignment = xlCenter
        .VerticalAlignment = xlBottom
        .WrapText = False
        .Orientation = 0
        .AddIndent = False
        .IndentLevel = 0
        .ShrinkToFit = False
        .ReadingOrder = xlContext
        .MergeCells = False
        .Font.Bold = True
    End With
    
    clickedCell(1, 4).Borders(xlDiagonalDown).LineStyle = xlNone
    Selection.Borders(xlDiagonalUp).LineStyle = xlNone
    With clickedCell(1, 4).Borders(xlEdgeLeft)
        .LineStyle = xlContinuous
        .ColorIndex = 0
        .TintAndShade = 0
        .Weight = xlMedium
    End With
    With clickedCell(1, 4).Borders(xlEdgeTop)
        .LineStyle = xlContinuous
        .ColorIndex = 0
        .TintAndShade = 0
        .Weight = xlMedium
    End With
    With clickedCell(1, 4).Borders(xlEdgeBottom)
        .LineStyle = xlContinuous
        .ColorIndex = 0
        .TintAndShade = 0
        .Weight = xlMedium
    End With
    With clickedCell(1, 4).Borders(xlEdgeRight)
        .LineStyle = xlContinuous
        .ColorIndex = 0
        .TintAndShade = 0
        .Weight = xlMedium
    End With
    clickedCell(1, 4).Borders(xlInsideVertical).LineStyle = xlNone
    clickedCell(1, 4).Borders(xlInsideHorizontal).LineStyle = xlNone
    With clickedCell(1, 4).Interior
        .Pattern = xlSolid
        .PatternColorIndex = xlAutomatic
        .ThemeColor = xlThemeColorDark2
        .Color = 16777164
        .PatternTintAndShade = 0
    End With
    
    clickedCell(1, 4).FormulaR1C1 = Format(0, "hh:mm:ss")
    
    Call ResetCursor(clickedCell)
    
End Sub

Private Sub ResetCursor(ByVal clickedCell As Range)

    Dim timerWksh As Worksheet
   
    ignoreClick = True
    
    Set timerWksh = clickedCell.Worksheet
    
    timerWksh.Cells(1, 1).Select
    
    ignoreClick = False
    
End Sub
