Imports System.ComponentModel

Public Class Form1

    Dim gridSize As Integer = 10
    Dim buttons(gridSize - 1, gridSize - 1) As Button
    Dim nextNumber As Integer = 1

    Private Sub Button_Click(sender As Object, e As EventArgs)
        Dim clickedButton As Button = CType(sender, Button)
        Dim pos As Point = CType(clickedButton.Tag, Point)
        Dim listPosition As List(Of Point)
        Dim result As Boolean = False
        ' Lógica para verificar regras de espaçamento
        ' Exemplo: verificar se há espaço horizontal, vertical e diagonal

        'desabilitar todos os botoes
        For i As Integer = 0 To gridSize - 1
            For j As Integer = 0 To gridSize - 1
                buttons(i, j).Enabled = False
                buttons(i, j).BackColor = Color.Blue
            Next
        Next

        listPosition = availablePosition(pos)

        For Each position In listPosition
            Try
                If buttons(position.X, position.Y) IsNot Nothing Then
                    If buttons(position.X, position.Y).Text = "" Then
                        buttons(position.X, position.Y).Enabled = True
                        buttons(position.X, position.Y).BackColor = Color.Lime
                        result = True
                    End If
                End If
            Catch
            End Try
        Next


        ' Colocar o número no botão
        If result Then
            clickedButton.Text = nextNumber.ToString()
            nextNumber += 1
        Else
            If verifyIfYouGain() Then
                MessageBox.Show("Você perdeu! Fim de Jogo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show("Você Ganhou", "Ganhador", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        End If



    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim number As String = Convert.ToInt32(TextBox1.Text)
        If number >= 4 And number <= 10 Then
            TableLayoutPanel1.Controls.Clear()
            gridSize = number
            TableLayoutPanel1.RowCount = gridSize
            TableLayoutPanel1.ColumnCount = gridSize
            TableLayoutPanel1.RowStyles.Clear()
            TableLayoutPanel1.ColumnStyles.Clear()

            For i As Integer = 0 To gridSize - 1
                TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100 / gridSize))
                TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100 / gridSize))
            Next

            ' Criar botões dinamicamente
            For i As Integer = 0 To gridSize - 1
                For j As Integer = 0 To gridSize - 1
                    buttons(i, j) = New Button With {
                    .Text = "", .BackColor = Color.Blue, .Dock = DockStyle.Fill, .Tag = New Point(i, j)
                }
                    AddHandler buttons(i, j).Click, AddressOf Button_Click
                    TableLayoutPanel1.Controls.Add(buttons(i, j), j, i)
                Next
            Next

        Else
            MessageBox.Show("Por favor, insira um número válido.")
        End If


    End Sub

    Private Function availablePosition(pos As Point) As List(Of Point)
        Dim x As Integer = pos.X
        Dim y As Integer = pos.Y
        Dim po1, po2, po3, po4, po5, po6, po7, po8 As Point
        Dim listaPoint As New List(Of Point)


        ' Horizontal
        po1.X = x
        po1.Y = y + 3

        po2.X = x
        po2.Y = y - 3

        ' Vertical

        po3.X = x + 3
        po3.Y = y

        po4.X = x - 3
        po4.Y = y

        ' Diagonal
        po5.X = x + 2
        po5.Y = y - 2

        po6.X = x - 2
        po6.Y = y - 2

        po7.X = x - 2
        po7.Y = y + 2

        po8.X = x + 2
        po8.Y = y + 2

        listaPoint.Add(po1)

        listaPoint.Add(po2)

        listaPoint.Add(po3)

        listaPoint.Add(po4)

        listaPoint.Add(po5)

        listaPoint.Add(po6)

        listaPoint.Add(po7)

        listaPoint.Add(po8)




        Return listaPoint
    End Function

    Private Function verifyIfYouGain() As Boolean
        Dim textIsEmpty As Boolean = False
        For i As Integer = 0 To gridSize - 1
            For j As Integer = 0 To gridSize - 1
                If buttons(i, j).Text = "" Then
                    Return True
                End If

            Next
        Next
        Return textIsEmpty

    End Function


    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then

            e.Handled = True
        End If
    End Sub

End Class
