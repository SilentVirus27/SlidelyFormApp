Imports System.Net.Http
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Inherits Form

    Private currentIndex As Integer = 0
    Private submissions As List(Of Submission)

    Public Sub New()
        Me.Text = "View Submissions"
        Me.Size = New Size(400, 400)
        Me.KeyPreview = True

        ' Add controls
        Dim lblName As New Label With {.Text = "Name", .Location = New Point(30, 30)}
        Me.Controls.Add(lblName)
        Dim txtName As New TextBox With {.Location = New Point(150, 30), .Size = New Size(200, 20), .Name = "NameTextBox", .ReadOnly = True}
        Me.Controls.Add(txtName)

        Dim lblEmail As New Label With {.Text = "Email", .Location = New Point(30, 70)}
        Me.Controls.Add(lblEmail)
        Dim txtEmail As New TextBox With {.Location = New Point(150, 70), .Size = New Size(200, 20), .Name = "EmailTextBox", .ReadOnly = True}
        Me.Controls.Add(txtEmail)

        Dim lblPhone As New Label With {.Text = "Phone Num", .Location = New Point(30, 110)}
        Me.Controls.Add(lblPhone)
        Dim txtPhone As New TextBox With {.Location = New Point(150, 110), .Size = New Size(200, 20), .Name = "PhoneTextBox", .ReadOnly = True}
        Me.Controls.Add(txtPhone)

        Dim lblGithub As New Label With {.Text = "Github Link For Task 2", .Location = New Point(30, 150)}
        Me.Controls.Add(lblGithub)
        Dim txtGithub As New TextBox With {.Location = New Point(150, 150), .Size = New Size(200, 20), .Name = "GitHubLinkTextBox", .ReadOnly = True}
        Me.Controls.Add(txtGithub)

        Dim lblStopwatch As New Label With {.Text = "Stopwatch time", .Location = New Point(30, 190)}
        Me.Controls.Add(lblStopwatch)
        Dim txtStopwatch As New TextBox With {.Location = New Point(150, 190), .Size = New Size(200, 20), .Name = "StopwatchLabel", .ReadOnly = True}
        Me.Controls.Add(txtStopwatch)

        Dim btnPrev As New Button With {.Text = "Previous (CTRL + P)", .Location = New Point(30, 230), .Size = New Size(150, 30), .Name = "PreviousButton"}
        AddHandler btnPrev.Click, AddressOf PreviousButton_Click
        Me.Controls.Add(btnPrev)

        Dim btnNext As New Button With {.Text = "Next (CTRL + N)", .Location = New Point(200, 230), .Size = New Size(150, 30), .Name = "NextButton"}
        AddHandler btnNext.Click, AddressOf NextButton_Click
        Me.Controls.Add(btnNext)

        LoadSubmissions()
    End Sub

    Private Async Sub LoadSubmissions()
        Dim client As New HttpClient()

        Try
            Dim response = Await client.GetAsync("http://localhost:3000/read?index=" & currentIndex)

            If response.IsSuccessStatusCode Then
                Dim json As String = Await response.Content.ReadAsStringAsync()
                Dim submission As Dictionary(Of String, String) = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(json)

                ' Update text boxes with submission data
                Me.Controls("NameTextBox").Text = submission("name")
                Me.Controls("EmailTextBox").Text = submission("email")
                Me.Controls("PhoneTextBox").Text = submission("phone")
                Me.Controls("GitHubLinkTextBox").Text = submission("github_link")
                Me.Controls("StopwatchLabel").Text = submission("stopwatch_time")
            Else
                MessageBox.Show($"Failed to fetch submissions. Status code: {response.StatusCode}")
            End If
        Catch ex As Exception
            MessageBox.Show($"Error fetching data: {ex.Message}")
        End Try
    End Sub

    Private Sub PreviousButton_Click(sender As Object, e As EventArgs)
        If currentIndex > 0 Then
            currentIndex -= 1
            LoadSubmissions()
        End If
    End Sub

    Private Sub NextButton_Click(sender As Object, e As EventArgs)
        ' You need to adjust the logic based on the number of submissions available
        ' For simplicity, assume there's always a next submission
        currentIndex += 1
        LoadSubmissions()
    End Sub
End Class
