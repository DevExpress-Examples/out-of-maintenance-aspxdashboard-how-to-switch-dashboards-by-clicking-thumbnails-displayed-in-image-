Imports System
Imports System.Data
Imports System.Collections.Generic

Namespace DashboardMainDemo
    Public Class HumanResourcesData
        Public Class HistoryItem
            Private retDate? As Date
            Private hirDate? As Date

            Public Property HiredDate() As Date?
                Get
                    Return hirDate
                End Get
                Set(ByVal value? As Date)
                    hirDate = value
                End Set
            End Property
            Public Property RetiredDate() As Date?
                Get
                    Return retDate
                End Get
                Set(ByVal value? As Date)
                    retDate = value
                End Set
            End Property

            Public Function IsEmployeed(ByVal dt As Date) As Boolean
                Return (Not HiredDate.HasValue OrElse HiredDate.Value <= dt) AndAlso (Not RetiredDate.HasValue OrElse RetiredDate.Value >= dt)
            End Function
            Public Function IsRetired(ByVal dt As Date) As Boolean
                Return RetiredDate.HasValue AndAlso RetiredDate.Value = dt
            End Function
        End Class

        Private Const FullYears As Integer = 9

        Private Shared Function GetEmployeeFullName(ByVal employee As DataRow) As String
            Return DirectCast(employee("FullName"), String)
        End Function
        Private Shared Function GetEmployeeDepartmentID(ByVal employee As DataRow) As Integer
            Return DirectCast(employee("DepartmentID"), Integer)
        End Function
        Private Shared Function GetDepartmentName(ByVal department As DataRow) As String
            Return DirectCast(department("DepartmentName"), String)
        End Function
        Private Shared Function GetDepartmentBaseSalary(ByVal department As DataRow) As Decimal
            Return DirectCast(department("BaseSalary"), Decimal)
        End Function

        Private ReadOnly employeesTable As DataTable
        Private ReadOnly departmentsTable As DataTable
        Private ReadOnly startDate As Date
        Private ReadOnly endDate As Date
        Private ReadOnly employeesHistory As New Dictionary(Of String, HistoryItem)()
        Private ReadOnly rand As New Random()
        Private ReadOnly deptData As New Dictionary(Of DepartmentDataKey, DepartmentData)()
        Private ReadOnly empData As New List(Of EmployeeData)()

        Private ReadOnly Property Employees() As DataRowCollection
            Get
                Return employeesTable.Rows
            End Get
        End Property
        Public ReadOnly Property DepartmentData() As IEnumerable(Of DepartmentData)
            Get
                Return deptData.Values
            End Get
        End Property
        Public ReadOnly Property EmployeeData() As IEnumerable(Of EmployeeData)
            Get
                Return empData
            End Get
        End Property

        Public Sub New(ByVal dataSet As DataSet)
            employeesTable = dataSet.Tables("Employees")
            departmentsTable = dataSet.Tables("Departments")
            endDate = New Date(Date.Today.Year, Date.Today.Month, 1)
            endDate = endDate.AddMonths(-1)
            startDate = New Date(endDate.Year - FullYears, 1, 1)
            CreateImployeesHistory()
            Dim dt As Date = startDate
            Do While dt <= endDate
                For Each employee As DataRow In Employees
                    Dim fullName As String = GetEmployeeFullName(employee)
                    Dim historyItem As HistoryItem = employeesHistory(fullName)
                    If historyItem.IsEmployeed(dt) Then
                        Dim departmentID As Integer = GetEmployeeDepartmentID(employee)
                        Dim department As DataRow = GetDepartmentByDepartmentID(departmentID)
                        Dim departmentName As String = GetDepartmentName(department)
                        Dim departmentDataKey As New DepartmentDataKey(dt, departmentName)
                        Dim departmentDataValue As DepartmentData = Nothing
                        If Not deptData.TryGetValue(departmentDataKey, departmentDataValue) Then
                            departmentDataValue = New DepartmentData With { _
                                .CurrentDate = dt, _
                                .Department = departmentName _
                            }
                            deptData.Add(departmentDataKey, departmentDataValue)
                        End If
                        departmentDataValue.HeadCount += 1
                        If historyItem.IsRetired(dt) Then
                            departmentDataValue.RetiredCount += 1
                        End If

                        Dim baseSalary As Decimal = GetDepartmentBaseSalary(department)
                        Dim salary As Decimal = baseSalary + CDec(DataHelper.Random(rand, CDbl(baseSalary) / rand.Next(1, 5)))
                        Dim bonus As Decimal = CDec(DataHelper.Random(rand, CDbl(salary), True))
                        Dim overtime As Decimal = CDec(DataHelper.Random(rand, CDbl(salary) / rand.Next(1, 5), True))

                        Dim vacationDays As Integer = 0
                        If rand.NextDouble() > 0.5 Then
                            vacationDays = rand.Next(0, 10)
                        End If
                        Dim sickLeaveDays As Integer = 0
                        If rand.NextDouble() > 0.5 Then
                            sickLeaveDays = rand.Next(0, 5)
                        End If

                        empData.Add(New EmployeeData With { _
                            .CurrentDate = dt, _
                            .Department = departmentName, _
                            .Employee = fullName, _
                            .Salary = salary, _
                            .Bonus = bonus, _
                            .Overtime = overtime, _
                            .VacationDays = vacationDays, _
                            .SickLeaveDays = sickLeaveDays _
                        })
                    End If
                Next employee
                dt = dt.AddMonths(1)
            Loop
            For Each data As DepartmentData In deptData.Values
                data.StaffTurnrover = If(data.HeadCount > 0, CDbl(data.RetiredCount) / data.HeadCount, 0)
                data.StaffTurnroverCritical = 0.01
            Next data
        End Sub
        Private Sub CreateImployeesHistory()
            Dim totalMonths As Integer = FullYears * 12 + endDate.Month
            For Each employee As DataRow In Employees
                Dim hiredDate? As Date = Nothing
                Dim hiredMonth As Integer = 0
                If rand.NextDouble() > 0.2 Then
                    hiredMonth = CInt((Math.Round(DataHelper.Random(rand, totalMonths, True))))
                    hiredDate = startDate.AddMonths(hiredMonth)
                End If
                Dim retiredDate? As Date = Nothing
                If rand.NextDouble() > 0.3 Then
                    Dim retiredMonth As Integer = CInt((Math.Round(DataHelper.Random(rand, totalMonths, True))))
                    If retiredMonth > hiredMonth Then
                        retiredDate = startDate.AddMonths(retiredMonth)
                    End If
                End If
                employeesHistory.Add(GetEmployeeFullName(employee), New HistoryItem With { _
                    .HiredDate = hiredDate, _
                    .RetiredDate = retiredDate _
                })
            Next employee
        End Sub
        Private Function GetDepartmentByDepartmentID(ByVal departmentID As Integer) As DataRow
            Return departmentsTable.Select(String.Format("DepartmentID = {0}", departmentID))(0)
        End Function
    End Class

    Public Class DepartmentData
        Private staffTurnCritical As Double
        Private staffTurn As Double
        Private retCount As Integer
        Private hCount As Integer
        Private dept As String
        Private curtDate As Date

        Public Property CurrentDate() As Date
            Get
                Return curtDate
            End Get
            Set(ByVal value As Date)
                curtDate = value
            End Set
        End Property
        Public Property Department() As String
            Get
                Return dept
            End Get
            Set(ByVal value As String)
                dept = value
            End Set
        End Property
        Public Property HeadCount() As Integer
            Get
                Return hCount
            End Get
            Set(ByVal value As Integer)
                hCount = value
            End Set
        End Property
        Public Property RetiredCount() As Integer
            Get
                Return retCount
            End Get
            Set(ByVal value As Integer)
                retCount = value
            End Set
        End Property
        Public Property StaffTurnrover() As Double
            Get
                Return staffTurn
            End Get
            Set(ByVal value As Double)
                staffTurn = value
            End Set
        End Property
        Public Property StaffTurnroverCritical() As Double
            Get
                Return staffTurnCritical
            End Get
            Set(ByVal value As Double)
                staffTurnCritical = value
            End Set
        End Property
    End Class

    Public Class DepartmentDataKey
        Private ReadOnly dt As Date
        Private ReadOnly dept As String

        Public Sub New(ByVal dt As Date, ByVal dept As String)
            Me.dt = dt
            Me.dept = dept
        End Sub
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim key As DepartmentDataKey = DirectCast(obj, DepartmentDataKey)
            Return key.dt = dt AndAlso key.dept = dept
        End Function
        Public Overrides Function GetHashCode() As Integer
            Return dt.GetHashCode() Xor dept.GetHashCode()
        End Function
    End Class

    Public Class EmployeeData
        Private sickLDays As Integer
        Private overt As Decimal
        Private vacDays As Integer
        Private bon As Decimal
        Private sal As Decimal
        Private emp As String
        Private dept As String
        Private curtDate As Date

        Public Property CurrentDate() As Date
            Get
                Return curtDate
            End Get
            Set(ByVal value As Date)
                curtDate = value
            End Set
        End Property
        Public Property Department() As String
            Get
                Return dept
            End Get
            Set(ByVal value As String)
                dept = value
            End Set
        End Property
        Public Property Employee() As String
            Get
                Return emp
            End Get
            Set(ByVal value As String)
                emp = value
            End Set
        End Property
        Public Property Salary() As Decimal
            Get
                Return sal
            End Get
            Set(ByVal value As Decimal)
                sal = value
            End Set
        End Property
        Public Property Bonus() As Decimal
            Get
                Return bon
            End Get
            Set(ByVal value As Decimal)
                bon = value
            End Set
        End Property
        Public Property Overtime() As Decimal
            Get
                Return overt
            End Get
            Set(ByVal value As Decimal)
                overt = value
            End Set
        End Property
        Public Property VacationDays() As Integer
            Get
                Return vacDays
            End Get
            Set(ByVal value As Integer)
                vacDays = value
            End Set
        End Property
        Public Property SickLeaveDays() As Integer
            Get
                Return sickLDays
            End Get
            Set(ByVal value As Integer)
                sickLDays = value
            End Set
        End Property
    End Class
End Namespace
