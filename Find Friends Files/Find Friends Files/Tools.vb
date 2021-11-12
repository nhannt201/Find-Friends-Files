Module Tools

    Public Function Split(ByVal input As String,
                          ByVal ParamArray delimiter As String()) As String()
        Return input.Split(delimiter, StringSplitOptions.None)
    End Function
End Module