Imports System
Imports System.Data
Imports System.Collections.Generic

Namespace DashboardMainDemo

    Public Class HumanResourcesData

        Public Class HistoryItem

            Private retDate As System.DateTime?

            Private hirDate As System.DateTime?

            Public Property HiredDate As System.DateTime?
                Get
                    Return Me.hirDate
                End Get

                Set(ByVal value As System.DateTime?)
                    Me.hirDate = value
                End Set
            End Property

            Public Property RetiredDate As System.DateTime?
                Get
                    Return Me.retDate
                End Get

                Set(ByVal value As System.DateTime?)
                    Me.retDate = value
                End Set
            End Property

            Public Function IsEmployeed(ByVal dt As System.DateTime) As Boolean
                Return(Not Me.HiredDate.HasValue OrElse Me.HiredDate.Value <= dt) AndAlso (Not Me.RetiredDate.HasValue OrElse Me.RetiredDate.Value >= dt)
            End Function

            Public Function IsRetired(ByVal dt As System.DateTime) As Boolean
                Return Me.RetiredDate.HasValue AndAlso Me.RetiredDate.Value = dt
            End Function
        End Class

        Const FullYears As Integer = 9

        Private Shared Function GetEmployeeFullName(ByVal employee As System.Data.DataRow) As String
            Return CStr(employee("FullName"))
        End Function

        Private Shared Function GetEmployeeDepartmentID(ByVal employee As System.Data.DataRow) As Integer
            Return CInt(employee("DepartmentID"))
        End Function

        Private Shared Function GetDepartmentName(ByVal department As System.Data.DataRow) As String
            Return CStr(department("DepartmentName"))
        End Function

        Private Shared Function GetDepartmentBaseSalary(ByVal department As System.Data.DataRow) As Decimal
            Return CDec(department("BaseSalary"))
        End Function

        Private ReadOnly employeesTable As System.Data.DataTable

        Private ReadOnly departmentsTable As System.Data.DataTable

        Private ReadOnly startDate As System.DateTime

        Private ReadOnly endDate As System.DateTime

        Private ReadOnly employeesHistory As System.Collections.Generic.Dictionary(Of String, DashboardMainDemo.HumanResourcesData.HistoryItem) = New System.Collections.Generic.Dictionary(Of String, DashboardMainDemo.HumanResourcesData.HistoryItem)()

        Private ReadOnly rand As System.Random = New System.Random()

        Private ReadOnly deptData As System.Collections.Generic.Dictionary(Of DashboardMainDemo.DepartmentDataKey, DashboardMainDemo.DepartmentData) = New System.Collections.Generic.Dictionary(Of DashboardMainDemo.DepartmentDataKey, DashboardMainDemo.DepartmentData)()

        Private ReadOnly empData As System.Collections.Generic.List(Of DashboardMainDemo.EmployeeData) = New System.Collections.Generic.List(Of DashboardMainDemo.EmployeeData)()

        Private ReadOnly Property Employees As DataRowCollection
            Get
                Return Me.employeesTable.Rows
            End Get
        End Property

        Public ReadOnly Property DepartmentData As IEnumerable(Of DashboardMainDemo.DepartmentData)
            Get
                Return Me.deptData.Values
            End Get
        End Property

        Public ReadOnly Property EmployeeData As IEnumerable(Of DashboardMainDemo.EmployeeData)
            Get
                Return Me.empData
            End Get
        End Property

        Public Sub New(ByVal dataSet As System.Data.DataSet)
            Me.employeesTable = dataSet.Tables("Employees")
            Me.departmentsTable = dataSet.Tables("Departments")
            Me.endDate = New System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1)
            Me.endDate = Me.endDate.AddMonths(-1)
            Me.startDate = New System.DateTime(Me.endDate.Year - DashboardMainDemo.HumanResourcesData.FullYears, 1, 1)
            Me.CreateImployeesHistory()
            Dim dt As System.DateTime = Me.startDate
            While dt <= Me.endDate
                For Each employee As System.Data.DataRow In Me.Employees
                    Dim fullName As String = DashboardMainDemo.HumanResourcesData.GetEmployeeFullName(employee)
                    Dim historyItem As DashboardMainDemo.HumanResourcesData.HistoryItem = Me.employeesHistory(fullName)
                    If historyItem.IsEmployeed(dt) Then
                        Dim departmentID As Integer = DashboardMainDemo.HumanResourcesData.GetEmployeeDepartmentID(employee)
                        Dim department As System.Data.DataRow = Me.GetDepartmentByDepartmentID(departmentID)
                        Dim departmentName As String = DashboardMainDemo.HumanResourcesData.GetDepartmentName(department)
                        Dim departmentDataKey As DashboardMainDemo.DepartmentDataKey = New DashboardMainDemo.DepartmentDataKey(dt, departmentName)
                        Dim departmentDataValue As DashboardMainDemo.DepartmentData = Nothing
                        If Not Me.deptData.TryGetValue(departmentDataKey, departmentDataValue) Then
                            departmentDataValue = New DashboardMainDemo.DepartmentData With {.CurrentDate = dt, .Department = departmentName}
                            Me.deptData.Add(departmentDataKey, departmentDataValue)
                        End If

                        departmentDataValue.HeadCount += 1
                        If historyItem.IsRetired(dt) Then departmentDataValue.RetiredCount += 1
                        Dim baseSalary As Decimal = DashboardMainDemo.HumanResourcesData.GetDepartmentBaseSalary(department)
                        Dim salary As Decimal = baseSalary + CDec(DashboardMainDemo.DataHelper.Random(Me.rand, CDbl(baseSalary) / Me.rand.[Next](1, 5)))
                        Dim bonus As Decimal = CDec(DashboardMainDemo.DataHelper.Random(Me.rand, CDbl(salary), True))
                        Dim overtime As Decimal = CDec(DashboardMainDemo.DataHelper.Random(Me.rand, CDbl(salary) / Me.rand.[Next](1, 5), True))
                        Dim vacationDays As Integer = 0
                        If Me.rand.NextDouble() > 0.5 Then vacationDays = Me.rand.[Next](0, 10)
                        Dim sickLeaveDays As Integer = 0
                        If Me.rand.NextDouble() > 0.5 Then sickLeaveDays = Me.rand.[Next](0, 5)
                        Me.empData.Add(New DashboardMainDemo.EmployeeData With {.CurrentDate = dt, .Department = departmentName, .Employee = fullName, .Salary = salary, .Bonus = bonus, .Overtime = overtime, .VacationDays = vacationDays, .SickLeaveDays = sickLeaveDays})
                    End If
                Next

                dt = dt.AddMonths(1)
            End While

            For Each data As DashboardMainDemo.DepartmentData In Me.deptData.Values
                data.StaffTurnrover = If(data.HeadCount > 0, CDbl(data.RetiredCount) / data.HeadCount, 0)
                data.StaffTurnroverCritical = 0.01
            Next
        End Sub

        Private Sub CreateImployeesHistory()
            Dim totalMonths As Integer = DashboardMainDemo.HumanResourcesData.FullYears * 12 + Me.endDate.Month
            For Each employee As System.Data.DataRow In Me.Employees
                Dim hiredDate As System.DateTime? = Nothing
                Dim hiredMonth As Integer = 0
                If Me.rand.NextDouble() > 0.2 Then
                    hiredMonth = CInt(System.Math.Round(DashboardMainDemo.DataHelper.Random(Me.rand, totalMonths, True)))
                    hiredDate = Me.startDate.AddMonths(hiredMonth)
                End If

                Dim retiredDate As System.DateTime? = Nothing
                If Me.rand.NextDouble() > 0.3 Then
                    Dim retiredMonth As Integer = CInt(System.Math.Round(DashboardMainDemo.DataHelper.Random(Me.rand, totalMonths, True)))
                    If retiredMonth > hiredMonth Then retiredDate = Me.startDate.AddMonths(retiredMonth)
                End If

                Me.employeesHistory.Add(DashboardMainDemo.HumanResourcesData.GetEmployeeFullName(employee), New DashboardMainDemo.HumanResourcesData.HistoryItem With {.HiredDate = hiredDate, .RetiredDate = retiredDate})
            Next
        End Sub

        Private Function GetDepartmentByDepartmentID(ByVal departmentID As Integer) As DataRow
            Return Me.departmentsTable.[Select](String.Format("DepartmentID = {0}", departmentID))(0)
        End Function
    End Class

    Public Class DepartmentData

        Private staffTurnCritical As Double

        Private staffTurn As Double

        Private retCount As Integer

        Private hCount As Integer

        Private dept As String

        Private curtDate As System.DateTime

        Public Property CurrentDate As DateTime
            Get
                Return Me.curtDate
            End Get

            Set(ByVal value As DateTime)
                Me.curtDate = value
            End Set
        End Property

        Public Property Department As String
            Get
                Return Me.dept
            End Get

            Set(ByVal value As String)
                Me.dept = value
            End Set
        End Property

        Public Property HeadCount As Integer
            Get
                Return Me.hCount
            End Get

            Set(ByVal value As Integer)
                Me.hCount = value
            End Set
        End Property

        Public Property RetiredCount As Integer
            Get
                Return Me.retCount
            End Get

            Set(ByVal value As Integer)
                Me.retCount = value
            End Set
        End Property

        Public Property StaffTurnrover As Double
            Get
                Return Me.staffTurn
            End Get

            Set(ByVal value As Double)
                Me.staffTurn = value
            End Set
        End Property

        Public Property StaffTurnroverCritical As Double
            Get
                Return Me.staffTurnCritical
            End Get

            Set(ByVal value As Double)
                Me.staffTurnCritical = value
            End Set
        End Property
    End Class

    Public Class DepartmentDataKey

        Private ReadOnly dt As System.DateTime

        Private ReadOnly dept As String

        Public Sub New(ByVal dt As System.DateTime, ByVal dept As String)
            Me.dt = dt
            Me.dept = dept
        End Sub

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim key As DashboardMainDemo.DepartmentDataKey = CType(obj, DashboardMainDemo.DepartmentDataKey)
            Return key.dt = Me.dt AndAlso Equals(key.dept, Me.dept)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Me.dt.GetHashCode() Xor Me.dept.GetHashCode()
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

        Private curtDate As System.DateTime

        Public Property CurrentDate As DateTime
            Get
                Return Me.curtDate
            End Get

            Set(ByVal value As DateTime)
                Me.curtDate = value
            End Set
        End Property

        Public Property Department As String
            Get
                Return Me.dept
            End Get

            Set(ByVal value As String)
                Me.dept = value
            End Set
        End Property

        Public Property Employee As String
            Get
                Return Me.emp
            End Get

            Set(ByVal value As String)
                Me.emp = value
            End Set
        End Property

        Public Property Salary As Decimal
            Get
                Return Me.sal
            End Get

            Set(ByVal value As Decimal)
                Me.sal = value
            End Set
        End Property

        Public Property Bonus As Decimal
            Get
                Return Me.bon
            End Get

            Set(ByVal value As Decimal)
                Me.bon = value
            End Set
        End Property

        Public Property Overtime As Decimal
            Get
                Return Me.overt
            End Get

            Set(ByVal value As Decimal)
                Me.overt = value
            End Set
        End Property

        Public Property VacationDays As Integer
            Get
                Return Me.vacDays
            End Get

            Set(ByVal value As Integer)
                Me.vacDays = value
            End Set
        End Property

        Public Property SickLeaveDays As Integer
            Get
                Return Me.sickLDays
            End Get

            Set(ByVal value As Integer)
                Me.sickLDays = value
            End Set
        End Property
    End Class
End Namespace
