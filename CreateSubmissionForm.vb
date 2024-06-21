Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class CreateSubmissionForm
    Private stopwatch As New Stopwatch()
    Private stopwatchTimer As New Timer()

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        Me.Text = "Create Submission"
        Me.Size = New Size(400, 400)

        ' Add controls
        Dim lblName As New Label With {.Text = "Name", .Location = New Point(30, 30)}
        Me.Controls.Add(lblName)
        Dim txtName As New TextBox With {.Location = New Point(150, 30), .Size = New Size(200, 20), .Name = "NameTextBox"}
        Me.Controls.Add(txtName)

        Dim lblEmail As New Label With {.Text = "Email", .Location = New Point(30, 70)}
        Me.Controls.Add(lblEmail)
        Dim txtEmail As New TextBox With {.Location = New Point(150, 70), .Size = New Size(200, 20), .Name = "EmailTextBox"}
        Me.Controls.Add(txtEmail)

        Dim lblPhone As New Label With {.Text = "Phone Num", .Location = New Point(30, 110)}
        Me.Controls.Add(lblPhone)
        Dim txtPhone As New TextBox With {.Location = New Point(150, 110), .Size = New Size(200, 20), .Name = "PhoneTextBox"}
        Me.Controls.Add(txtPhone)

        Dim lblGithub As New Label With {.Text = "Github Link For Task 2", .Location = New Point(30, 150)}
        Me.Controls.Add(lblGithub)
        Dim txtGithub As New TextBox With {.Location = New Point(150, 150), .Size = New Size(200, 20), .Name = "GitHubLinkTextBox"}
        Me.Controls.Add(txtGithub)

        Dim lblStopwatch As New Label With {.Text = "Stopwatch time", .Location = New Point(30, 190)}
        Me.Controls.Add(lblStopwatch)
        Dim txtStopwatch As New TextBox With {.Location = New Point(150, 190), .Size = New Size(200, 20), .Name = "StopwatchLabel", .ReadOnly = True}
        Me.Controls.Add(txtStopwatch)

        Dim btnToggleStopwatch As New Button With {.Text = "TOGGLE STOPWATCH (CTRL + T)", .Location = New Point(30, 230), .Size = New Size(200, 30), .Name = "ToggleStopwatchButton"}
        AddHandler btnToggleStopwatch.Click, AddressOf ToggleStopwatchButton_Click
        Me.Controls.Add(btnToggleStopwatch)

        Dim btnSubmit As New Button With {.Text = "SUBMIT (CTRL + S)", .Location = New Point(150, 270), .Size = New Size(200, 30), .Name = "SubmitButton"}
        AddHandler btnSubmit.Click, AddressOf SubmitButton_Click
        Me.Controls.Add(btnSubmit)

        ' Timer settings
        AddHandler stopwatchTimer.Tick, AddressOf UpdateStopwatchLabel
        stopwatchTimer.Interval = 1000 ' Update every second
    End Sub

    Private Sub UpdateStopwatchLabel(sender As Object, e As EventArgs)
        Me.Controls("StopwatchLabel").Text = String.Format("{0:hh\:mm\:ss}", stopwatch.Elapsed)
    End Sub

    Private Sub ToggleStopwatchButton_Click(sender As Object, e As EventArgs)
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            stopwatchTimer.Stop()
        Else
            stopwatch.Start()
            stopwatchTimer.Start()
        End If
    End Sub

    Private Async Sub SubmitButton_Click(sender As Object, e As EventArgs)
        Dim client As New HttpClient()
        Dim formData As New Dictionary(Of String, String) From {
            {"name", Me.Controls("NameTextBox").Text},
            {"email", Me.Controls("EmailTextBox").Text},
            {"phone", Me.Controls("PhoneTextBox").Text},
            {"github_link", Me.Controls("GitHubLinkTextBox").Text},
            {"stopwatch_time", Me.Controls("StopwatchLabel").Text}
        }
        Dim json As String = JsonConvert.SerializeObject(formData)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        Try
            Dim response = Await client.PostAsync("http://localhost:3000/submit", content)

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission successful!")
            Else
                MessageBox.Show("Submission failed.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error submitting data: " & ex.Message)
        End Try
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.S Then
            SubmitButton_Click(sender, e)
        ElseIf e.Control AndAlso e.KeyCode = Keys.T Then
            ToggleStopwatchButton_Click(sender, e)
        End If
    End Sub
End Class
