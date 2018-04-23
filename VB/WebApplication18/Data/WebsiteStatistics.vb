Imports System.Data
Imports System.Linq
Imports System
Imports System.Collections.Generic

Namespace DashboardMainDemo
    Public Class WebsiteStatisticsDataGenerator
        Public Class WebsiteStatisticsItem

            Private count_Renamed As Integer

            Private date_Renamed As Date

            Private trafficSource_Renamed As String

            Private trafficSourceDetails_Renamed As String

            Private browser_Renamed As String

            Private browserDetails_Renamed As String

            Public Property Count() As Integer
                Get
                    Return count_Renamed
                End Get
                Set(ByVal value As Integer)
                    count_Renamed = value
                End Set
            End Property
            Public Property [Date]() As Date
                Get
                    Return date_Renamed
                End Get
                Set(ByVal value As Date)
                    date_Renamed = value
                End Set
            End Property
            Public Property TrafficSource() As String
                Get
                    Return trafficSource_Renamed
                End Get
                Set(ByVal value As String)
                    trafficSource_Renamed = value
                End Set
            End Property
            Public Property TrafficSourceDetails() As String
                Get
                    Return trafficSourceDetails_Renamed
                End Get
                Set(ByVal value As String)
                    trafficSourceDetails_Renamed = value
                End Set
            End Property
            Public Property Browser() As String
                Get
                    Return browser_Renamed
                End Get
                Set(ByVal value As String)
                    browser_Renamed = value
                End Set
            End Property
            Public Property BrowserDetails() As String
                Get
                    Return browserDetails_Renamed
                End Get
                Set(ByVal value As String)
                    browserDetails_Renamed = value
                End Set
            End Property
        End Class
        Private Interface IChanceInterval
            Property Chance() As Double
        End Interface
        Private Class DataPairElement
            Implements IChanceInterval


            Private data_Renamed As String

            Private dataDetails_Renamed As String

            Private chance_Renamed As Double
            Public Property Data() As String
                Get
                    Return data_Renamed
                End Get
                Set(ByVal value As String)
                    data_Renamed = value
                End Set
            End Property
            Public Property DataDetails() As String
                Get
                    Return dataDetails_Renamed
                End Get
                Set(ByVal value As String)
                    dataDetails_Renamed = value
                End Set
            End Property
            Public Property Chance() As Double
                Get
                    Return chance_Renamed
                End Get
                Set(ByVal value As Double)
                    chance_Renamed = value
                End Set
            End Property
            Private Property IChanceInterval_Chance() As Double Implements IChanceInterval.Chance
                Get
                    Return chance_Renamed
                End Get
                Set(ByVal value As Double)
                    chance_Renamed = value
                End Set
            End Property
        End Class
        Private Class UserDataElement
            Implements IChanceInterval


            Private userId_Renamed As String

            Private chance_Renamed As Double
            Public Property UserId() As String
                Get
                    Return userId_Renamed
                End Get
                Set(ByVal value As String)
                    userId_Renamed = value
                End Set
            End Property
            Public Property Chance() As Double
                Get
                    Return chance_Renamed
                End Get
                Set(ByVal value As Double)
                    chance_Renamed = value
                End Set
            End Property
            Private Property IChanceInterval_Chance() As Double Implements IChanceInterval.Chance
                Get
                    Return chance_Renamed
                End Get
                Set(ByVal value As Double)
                    chance_Renamed = value
                End Set
            End Property
        End Class

        Private ReadOnly rand As New Random()
        Private ReadOnly items As New List(Of WebsiteStatisticsItem)()
        Public ReadOnly Property WebsiteStatistics() As IEnumerable(Of WebsiteStatisticsItem)
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
            Do While currentDate < endDate
                Dim monthModifier As Double = 1 + 0.03 * Math.Abs(currentDate.Month - 6)
                Dim baseCount As Integer = rand.Next(100000, 150000)
                For Each browserData As DataPairElement In browsersDataList
                    For Each trafficData As DataPairElement In dataTrafficSourceList
                        Dim item As New WebsiteStatisticsItem()
                        item.Count = Convert.ToInt32(baseCount * (browserData.Chance / 100) * (trafficData.Chance / 100) * monthModifier)
                        item.Date = currentDate
                        item.TrafficSource = trafficData.Data
                        item.TrafficSourceDetails = trafficData.DataDetails
                        item.Browser = browserData.Data
                        item.BrowserDetails = browserData.DataDetails
                        items.Add(item)
                    Next trafficData
                Next browserData
                currentDate = currentDate.AddMonths(1)
            Loop
        End Sub
        Private Function GetUsersData(ByVal count As Integer) As IList(Of UserDataElement)
            Dim result As IList(Of UserDataElement) = Enumerable.Range(0, count).Select(Function(i) New UserDataElement() With { _
                .UserId = Guid.NewGuid().ToString(), _
                .Chance = rand.Next(1, 3) _
            }).ToList()
            InitChance(result.Cast(Of IChanceInterval)().ToList())
            Return result
        End Function
        Private Function GetBrowserData() As IList(Of DataPairElement)
            Dim result As IList(Of DataPairElement) = New List(Of DataPairElement)()
            result.Add(New DataPairElement() With { _
                .Chance = 2.6, _
                .Data = "IE", _
                .DataDetails = "8" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 4.7, _
                .Data = "IE", _
                .DataDetails = "9" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 5.3, _
                .Data = "IE", _
                .DataDetails = "11" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 0.3, _
                .Data = "IE", _
                .DataDetails = "Others" _
            })

            result.Add(New DataPairElement() With { _
                .Chance = 38.0, _
                .Data = "Chrome", _
                .DataDetails = "Latest" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 17.3, _
                .Data = "Chrome", _
                .DataDetails = "Others" _
            })

            result.Add(New DataPairElement() With { _
                .Chance = 11.4, _
                .Data = "Firefox", _
                .DataDetails = "Latest" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 6.4, _
                .Data = "Firefox", _
                .DataDetails = "Others" _
            })

            result.Add(New DataPairElement() With { _
                .Chance = 1.7, _
                .Data = "Safari", _
                .DataDetails = "Others" _
            })

            result.Add(New DataPairElement() With { _
                .Chance = 0.7, _
                .Data = "Opera", _
                .DataDetails = "O Mini" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 3.0, _
                .Data = "Opera", _
                .DataDetails = "Others" _
            })

            InitChance(result.Cast(Of IChanceInterval)().ToList())
            Return result
        End Function
        Private Function GetTrafficSourceData() As IList(Of DataPairElement)
            Dim result As IList(Of DataPairElement) = New List(Of DataPairElement)()
            result.Add(New DataPairElement() With { _
                .Chance = 51.0, _
                .Data = "Direct", _
                .DataDetails = "Direct" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 19.0, _
                .Data = "Referring Site", _
                .DataDetails = "Facebook" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 02.0, _
                .Data = "Referring Site", _
                .DataDetails = "Google Ads" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 10.0, _
                .Data = "Referring Site", _
                .DataDetails = "Google+" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 13.3, _
                .Data = "Referring Site", _
                .DataDetails = "Twitter" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 02.3, _
                .Data = "Referring Site", _
                .DataDetails = "LinkedIn" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 03.3, _
                .Data = "Search Engine", _
                .DataDetails = "Bing" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 10.3, _
                .Data = "Search Engine", _
                .DataDetails = "Google" _
            })
            result.Add(New DataPairElement() With { _
                .Chance = 02.3, _
                .Data = "Search Engine", _
                .DataDetails = "Yahoo" _
            })
            InitChance(result.Cast(Of IChanceInterval)().ToList())
            Return result
        End Function
        Private Sub InitChance(ByVal dataList As IList(Of IChanceInterval))
            Dim sum As Double = dataList.Sum(Function(d) d.Chance)
            For Each data As IChanceInterval In dataList
                data.Chance = 100 * data.Chance / sum
            Next data
        End Sub
    End Class
End Namespace
