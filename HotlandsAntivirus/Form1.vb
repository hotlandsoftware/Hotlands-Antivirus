Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic.CompilerServices

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            ListBox1.Items.Clear()
            Button4.Visible = True
        Else
            Exit Sub
        End If

        On Error Resume Next

        For Each strFile As String In System.IO.Directory.GetFiles(FolderBrowserDialog1.SelectedPath, "*.*", IO.SearchOption.AllDirectories)
            ListBox1.Items.Add(strFile)
        Next
        Timer1.Start()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Maximum = Conversions.ToString(ListBox1.Items.Count)
        lblTotal.Text = Conversions.ToString(ListBox1.Items.Count)
        ListBox1.Enabled = False
        If Not ProgressBar1.Value = ProgressBar1.Maximum Then
            Try
                ListBox1.SelectedIndex = ListBox1.SelectedIndex + 1
                lblLast.Text = ListBox1.SelectedItem.ToString
            Catch ex As Exception
            End Try

            Try

                Dim scanbox As New TextBox
                Dim read As String = My.Computer.FileSystem.ReadAllText("main.db").ToString
                ProgressBar1.Increment(1)
                lblVirus.Text = Conversions.ToString(ListBox2.Items.Count)
                lblTotal.Text = Conversions.ToString(ProgressBar1.Value)
                scanbox.Text = read.ToString
                Dim md5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider
                Dim shit As FileStream = New FileStream(ListBox1.SelectedItem, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
                'shit = New FileStream(ListBox1.SelectedItem, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
                md5.ComputeHash(shit)
                shit.Close()
                Dim hash As Byte() = md5.Hash
                Dim buff As StringBuilder = New StringBuilder
                Dim hashByte As Byte
                For Each hashByte In hash
                    buff.Append(String.Format("{0:X2}", hashByte))
                Next

                If scanbox.Text.Contains(buff.ToString) Then
                    ListBox2.Items.Add(ListBox1.SelectedItem)
                End If
            Catch ex As Exception
            End Try
        Else
            ListBox1.Enabled = True
            Timer1.Stop()
            If ListBox2.Items.Count > 0 Then
                MsgBox("Scanning has been completed. There was " & vbCrLf & ListBox2.Items.Count & " viruses detected. Please review the list and choose an action.", MsgBoxStyle.Critical)
                Button4.Visible = False
                Button2.Visible = True
                Button3.Visible = True
                Exit Sub
            End If
            MsgBox("Scanning has been completed." & vbCrLf & "No viruses found.", MsgBoxStyle.Information)
            ProgressBar1.Value = 0
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ListBox2.Enabled = False
        ListBox2.SelectedIndex = -1
        If ListBox2.Items.Count = 0 Then
            MsgBox("...the fuck you doin?", MsgBoxStyle.Critical)
            Exit Sub
        End If

        While ListBox2.Items.Count - 1 = ListBox1.SelectedIndex = False
            ListBox2.SelectedIndex += 1
            File.Delete(ListBox2.SelectedItem)
            If ListBox2.Items.Count = ListBox2.SelectedIndex + 1 Then
                ListBox2.Items.Clear()
                ListBox2.Enabled = True
                MsgBox("All viruses have been removed. Your computer is now secured.", MsgBoxStyle.Information)
                ProgressBar1.Value = 0
                Button4.Visible = False
                Button2.Visible = False
                Button3.Visible = False
                Exit Sub
            End If
        End While
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MsgBox("Are you sure you want to ignore ALL items?", MsgBoxStyle.YesNo, "Woah there, fella!") = MsgBoxResult.Yes Then
            ListBox2.Items.Clear()
            ListBox2.Enabled = True
            ProgressBar1.Value = 0
            Button4.Visible = False
            Button2.Visible = False
            Button3.Visible = False
        Else
            'Absolutely FUCKING nothing!
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If MsgBox("Are you sure you want to abort the scan?", MsgBoxStyle.YesNo, "Woah there, fella!") = MsgBoxResult.Yes Then
            Timer1.Stop()
            If ListBox2.Items.Count > 0 Then
                MsgBox("Scanning has been completed. There was" & vbCrLf & ListBox2.Items.Count & " viruses detected. Please review the list and choose an action.", MsgBoxStyle.Critical)
                Timer1.Stop()
                ProgressBar1.Value = 0
                Button4.Visible = False
                Button2.Visible = True
                Button3.Visible = True
            Else
                MsgBox("Scanning has been completed." & vbCrLf & "No viruses found.", MsgBoxStyle.Information)
                Timer1.Stop()
                ProgressBar1.Value = 0
                Button4.Visible = False
            End If
        Else
            'Absolutely FUCKING nothing!
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            ListBox1.Items.Add(OpenFileDialog1.FileName)
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        SelectDrive.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Notepad.Show()
    End Sub
End Class
