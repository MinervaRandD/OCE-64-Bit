
Private Sub Worksheet_SelectionChange(ByVal Target As Range)

    Call Click_Handler(Target, IsAltKeyDown)

End Sub

