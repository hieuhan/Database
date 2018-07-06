Imports System.Data.Common 
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql 

Namespace SMSSystem.Common
    Public Class ErrorLogs
        Const connectionInstanceString As String = "SMSConnectionString"
        Public Shared Sub LogDb(ByVal ProcID As Int32, ByVal ProcName As String, ByVal ErrorDesc As String, ByVal ErrorLevelID As Int16)
            Dim db As Database = DatabaseFactory.CreateDatabase(connectionInstanceString)
            Try
                Dim cmd As DbCommand = db.GetStoredProcCommand("SystemErrors_Insert")
                db.AddInParameter(cmd, "ProcID", DbType.Int32, ProcID)
                db.AddInParameter(cmd, "ProcName", DbType.String, ProcName)
                db.AddInParameter(cmd, "ErrorDesc", DbType.String, ErrorDesc)
                db.AddInParameter(cmd, "ErrorLevelID", DbType.Int16, ErrorLevelID)
                db.ExecuteNonQuery(cmd)
            Catch ex As Exception
                WriteLog(ex.ToString)
            End Try
        End Sub

        Public Shared Function GetSystemMessageContent(ByVal SystemMessageID As Integer) As String
            Dim db As Database = DatabaseFactory.CreateDatabase(connectionInstanceString)
            Try
                Dim cmd As DbCommand = db.GetStoredProcCommand("SystemMessages_GetByID")
                db.AddInParameter(cmd, "SystemMessageID", DbType.Int64, SystemMessageID)
                Dim ds As DataSet = db.ExecuteDataSet(cmd)
                Dim SystemMessageDesc As String = ""
                If ds.Tables(0).Rows.Count > 0 Then
                    SystemMessageDesc = ds.Tables(0).Rows(0).Item("SystemMessageDesc")
                End If
                Return SystemMessageDesc
            Catch ex As Exception
                WriteLog(ex.ToString)
                Return ""
            End Try
        End Function

        Private Shared Sub WriteLog(ByVal strlog As String)
            Dim filename As String = ConfigurationManager.AppSettings("LogPath") & DateTime.Now.ToShortDateString.Replace("/", "-") & ".txt"
            Dim fi As New FileInfo(filename)
            Dim w As StreamWriter = File.AppendText(filename)
            If fi.Length < 5000000 Then
                w.WriteLine(Now.ToString & ":" & strlog)
                w.Flush()
            End If
            w.Close()
        End Sub
    End Class
End Namespace

