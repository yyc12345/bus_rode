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
            'type your code here
            Return ""
        End Function

    End Class

End Namespace
