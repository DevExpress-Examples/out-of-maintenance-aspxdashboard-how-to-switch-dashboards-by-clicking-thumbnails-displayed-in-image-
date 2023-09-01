Imports System.Data
Imports System.Linq
Imports System
Imports System.Collections.Generic

Namespace DashboardMainDemo

    Public Class WebsiteStatisticsDataGenerator

        Public Class WebsiteStatisticsItem

            Private countField As Integer

            Private dateField As Date

            Private trafficSourceField As String

            Private trafficSourceDetailsField As String

            Private browserField As String

            Private browserDetailsField As String

            Public Property Count As Integer
                Get
                    Return countField
                End Get

                Set(ByVal value As Integer)
                    countField = value
                End Set
            End Property

            Public Property [Date] As Date
                Get
                    Return dateField
                End Get

                Set(ByVal value As Date)
                    dateField = value
                End Set
            End Property

            Public Property TrafficSource As String
                Get
                    Return trafficSourceField
                End Get

                Set(ByVal value As String)
                    trafficSourceField = value
                End Set
            End Property

            Public Property TrafficSourceDetails As String
                Get
                    Return trafficSourceDetailsField
                End Get

                Set(ByVal value As String)
                    trafficSourceDetailsField = value
                End Set
            End Property

            Public Property Browser As String
                Get
                    Return browserField
                End Get

                Set(ByVal value As String)
                    browserField = value
                End Set
            End Property

            Public Property BrowserDetails As String
                Get
                    Return browserDetailsField
                End Get

                Set(ByVal value As String)
                    browserDetailsField = value
                End Set
            End Property
        End Class

        Friend Interface IChanceInterval

            Property Chance As Double

        End Interface

        Private Class DataPairElement
            Implements IChanceInterval

            Private dataField As String

            Private dataDetailsField As String

            Private chanceField As Double

            Public Property Data As String
                Get
                    Return dataField
                End Get

                Set(ByVal value As String)
                    dataField = value
                End Set
            End Property

            Public Property DataDetails As String
                Get
                    Return dataDetailsField
                End Get

                Set(ByVal value As String)
                    dataDetailsField = value
                End Set
            End Property

            Public Property ChanceProp As Double
                Get
                    Return chanceField
                End Get

                Set(ByVal value As Double)
                    chanceField = value
                End Set
            End Property

            Private Property Chance As Double Implements IChanceInterval.Chance
                Get
                    Return chanceField
                End Get

                Set(ByVal value As Double)
                    chanceField = value
                End Set
            End Property
        End Class

        Private Class UserDataElement
            Implements IChanceInterval

            Private userIdField As String

            Private chanceField As Double

            Public Property UserId As String
                Get
                    Return userIdField
                End Get

                Set(ByVal value As String)
                    userIdField = value
                End Set
            End Property

            Public Property ChanceProp As Double
                Get
                    Return chanceField
                End Get

                Set(ByVal value As Double)
                    chanceField = value
                End Set
            End Property

            Private Property Chance As Double Implements IChanceInterval.Chance
                Get
                    Return chanceField
                End Get

                Set(ByVal value As Double)
                    chanceField = value
                End Set
            End Property
        End Class

        Private ReadOnly rand As Random = New Random()

        Private ReadOnly items As List(Of WebsiteStatisticsItem) = New List(Of WebsiteStatisticsItem)()

        Public ReadOnly Property WebsiteStatistics As IEnumerable(Of WebsiteStatisticsItem)
            Get
                Return items
            End Get
        End Property

        Public Sub New()
            InitializeData()
        End Sub

        Private Sub InitializeData()
            Dim dataTrafficSourceList As IList(Of DataPairElement) = GetTrafficSourceData()
            Dim browsersDataList As IList(Of DataPairElement) = GetBrowserData()
            Dim usersDataList As IList(Of UserDataElement) = GetUsersData(10000)
            Dim currentDate As Date = Date.Today.AddYears(-1)
            Dim endDate As Date = Date.Today.AddDays(-1)
            While currentDate < endDate
                Dim monthModifier As Double = 1 + 0.03 * Math.Abs(currentDate.Month - 6)
                Dim baseCount As Integer = rand.Next(100000, 150000)
                For Each browserData As DataPairElement In browsersDataList
                    For Each trafficData As DataPairElement In dataTrafficSourceList
                        Dim item As WebsiteStatisticsItem = New WebsiteStatisticsItem()
                        item.Count = Convert.ToInt32(baseCount * (browserData.ChanceProp / 100) * (trafficData.ChanceProp / 100) * monthModifier)
                        item.Date = currentDate
                        item.TrafficSource = trafficData.Data
                        item.TrafficSourceDetails = trafficData.DataDetails
                        item.Browser = browserData.Data
                        item.BrowserDetails = browserData.DataDetails
                        items.Add(item)
                    Next
                Next

                currentDate = currentDate.AddMonths(1)
            End While
        End Sub

        Private Function GetUsersData(ByVal count As Integer) As IList(Of UserDataElement)
            Dim result As IList(Of UserDataElement) = Enumerable.Range(0, count).[Select](Function(i) New UserDataElement() With {.UserId = Guid.NewGuid().ToString(), .ChanceProp = rand.Next(1, 3)}).ToList()
            InitChance(result.Cast(Of IChanceInterval)().ToList())
            Return result
        End Function

        Private Function GetBrowserData() As IList(Of DataPairElement)
            Dim result As IList(Of DataPairElement) = New List(Of DataPairElement)()
            result.Add(New DataPairElement() With {.ChanceProp = 2.6, .Data = "IE", .DataDetails = "8"})
            result.Add(New DataPairElement() With {.ChanceProp = 4.7, .Data = "IE", .DataDetails = "9"})
            result.Add(New DataPairElement() With {.ChanceProp = 5.3, .Data = "IE", .DataDetails = "11"})
            result.Add(New DataPairElement() With {.ChanceProp = 0.3, .Data = "IE", .DataDetails = "Others"})
            result.Add(New DataPairElement() With {.ChanceProp = 38.0, .Data = "Chrome", .DataDetails = "Latest"})
            result.Add(New DataPairElement() With {.ChanceProp = 17.3, .Data = "Chrome", .DataDetails = "Others"})
            result.Add(New DataPairElement() With {.ChanceProp = 11.4, .Data = "Firefox", .DataDetails = "Latest"})
            result.Add(New DataPairElement() With {.ChanceProp = 6.4, .Data = "Firefox", .DataDetails = "Others"})
            result.Add(New DataPairElement() With {.ChanceProp = 1.7, .Data = "Safari", .DataDetails = "Others"})
            result.Add(New DataPairElement() With {.ChanceProp = 0.7, .Data = "Opera", .DataDetails = "O Mini"})
            result.Add(New DataPairElement() With {.ChanceProp = 3.0, .Data = "Opera", .DataDetails = "Others"})
            InitChance(result.Cast(Of IChanceInterval)().ToList())
            Return result
        End Function

        Private Function GetTrafficSourceData() As IList(Of DataPairElement)
            Dim result As IList(Of DataPairElement) = New List(Of DataPairElement)()
            result.Add(New DataPairElement() With {.ChanceProp = 51.0, .Data = "Direct", .DataDetails = "Direct"})
            result.Add(New DataPairElement() With {.ChanceProp = 19.0, .Data = "Referring Site", .DataDetails = "Facebook"})
            result.Add(New DataPairElement() With {.ChanceProp = 02.0, .Data = "Referring Site", .DataDetails = "Google Ads"})
            result.Add(New DataPairElement() With {.ChanceProp = 10.0, .Data = "Referring Site", .DataDetails = "Google+"})
            result.Add(New DataPairElement() With {.ChanceProp = 13.3, .Data = "Referring Site", .DataDetails = "Twitter"})
            result.Add(New DataPairElement() With {.ChanceProp = 02.3, .Data = "Referring Site", .DataDetails = "LinkedIn"})
            result.Add(New DataPairElement() With {.ChanceProp = 03.3, .Data = "Search Engine", .DataDetails = "Bing"})
            result.Add(New DataPairElement() With {.ChanceProp = 10.3, .Data = "Search Engine", .DataDetails = "Google"})
            result.Add(New DataPairElement() With {.ChanceProp = 02.3, .Data = "Search Engine", .DataDetails = "Yahoo"})
            InitChance(result.Cast(Of IChanceInterval)().ToList())
            Return result
        End Function

        Private Sub InitChance(ByVal dataList As IList(Of IChanceInterval))
            Dim sum As Double = dataList.Sum(Function(d) d.Chance)
            For Each data As IChanceInterval In dataList
                data.Chance = 100 * data.Chance / sum
            Next
        End Sub
    End Class
End Namespace
