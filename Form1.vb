Public Class Form1
    Private WithEvents btnViewSubmissions As Button
    Private WithEvents btnCreateSubmission As Button

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

        ' Initialize View Submissions button
        btnViewSubmissions = New Button With {
            .Text = "View Submissions (CTRL + V)",
            .Location = New Point(100, 50),
            .Size = New Size(200, 50),
            .Name = "btnViewSubmissions"
        }
        AddHandler btnViewSubmissions.Click, AddressOf btnViewSubmissions_Click
        Me.Controls.Add(btnViewSubmissions)

        ' Initialize Create New Submission button
        btnCreateSubmission = New Button With {
            .Text = "Create New Submission (CTRL + N)",
            .Location = New Point(100, 120),
            .Size = New Size(200, 50),
            .Name = "btnCreateSubmission"
        }
        AddHandler btnCreateSubmission.Click, AddressOf btnCreateSubmission_Click
        Me.Controls.Add(btnCreateSubmission)
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateSubmission.PerformClick()
        End If
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs)
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs)
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub
End Class
