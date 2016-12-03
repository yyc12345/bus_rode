Imports System.Text.RegularExpressions

Namespace BusRodeDll

    Public Class ResourcesDll

        Public Sub New()

            DllDependBusRodeVersion = 9000
            DllWriter = "Sample Name"
            DllRegion = "Country-Province-City"
            DllVersion = "1.0.0.0"
            DllLanguage = "en-US"

        End Sub

        'here are some information which will read when bus_rode is initializing.
        'you must know the mean of them and you need to read the book of develop bus_rode's mod.
#Region "information"

        Public DllDependBusRodeVersion As Integer

        Public DllRegion As String

        Public DllWriter As String

        Public DllVersion As String

        Public DllLanguage As String
#End Region


        ''' <summary>
        ''' the value which main application send it order
        ''' </summary>
        Public DllCommand As String


        ''' <summary>
        ''' the function which will be invoked when main application initialize this mode
        ''' </summary>
        ''' <returns></returns>
        Public Function Initialize() As Boolean
            'type your code here
            Return True
        End Function


        ''' <summary>
        ''' the main function of getting data
        ''' </summary>
        ''' <returns></returns>
        Public Function GetData() As String
            'do NOT change this code in this function.
            'if you want to process input data. you can use the sample without automatic process.
            'and you need to read the command's format.

            'try split command
            Dim spCommand = Regex.Split(DllCommand, "@", RegexOptions.IgnoreCase)

            Select Case spCommand(0)
                Case "GetLine"
                    Return GetLine(spCommand(1))
                Case "GetStop"
                    Return GetStop(spCommand(1))
                Case "GetLineList"
                    Return GetLineList(System.Convert.ToInt32(spCommand(1)))
                Case "GetStopList"
                    Return GetStopList(System.Convert.ToInt32(spCommand(1)))
                Case "GetSubway"
                    Return GetSubway(spCommand(1))
                Case Else
                    Return ""
            End Select

        End Function


        'store actual function which get data. please type your code here.
#Region "actual function"

        Private Function GetLine(lineName As String) As String
            'type your code here
            Return ""
        End Function

        Private Function GetStop(stopName As String) As String
            'type your code here
            Return ""
        End Function

        Private Function GetLineList(indexOfBlock As Integer) As String
            'type your code here
            Return ""
        End Function

        Private Function GetStopList(indexOfBlock As Integer) As String
            'type your code here
            Return ""
        End Function

        Private Function GetSubway(stopName As String) As String
            'type your code here
            Return ""
        End Function

#End Region

    End Class

End Namespace
