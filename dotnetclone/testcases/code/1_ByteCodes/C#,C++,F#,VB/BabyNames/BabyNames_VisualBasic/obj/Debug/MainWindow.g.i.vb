﻿#ExternalChecksum("..\..\MainWindow.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","9743DC0902857E1F58C1DC6F46A51D21")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.261
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports BabyNames
Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Shell

Namespace BabyNames
    
    '''<summary>
    '''Window1
    '''</summary>
    <Microsoft.VisualBasic.CompilerServices.DesignerGenerated(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")>  _
    Partial Public Class Window1
        Inherits System.Windows.Window
        Implements System.Windows.Markup.IComponentConnector
        
        
        #ExternalSource("..\..\MainWindow.xaml",5)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents MainWindow As BabyNames.Window1
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",7)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents MainGrid As System.Windows.Controls.Grid
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",25)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents btnRunLinq As System.Windows.Controls.Button
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",29)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents btnRunPlinq As System.Windows.Controls.Button
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",34)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents lblSpeedup As System.Windows.Controls.Label
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",38)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents txtQueryName As System.Windows.Controls.TextBox
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",39)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents lblQueryName As System.Windows.Controls.Label
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",40)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents txtQueryState As System.Windows.Controls.TextBox
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",41)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents lblQueryState As System.Windows.Controls.Label
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",42)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents slProcessorsToUse As System.Windows.Controls.Slider
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",43)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents lblProcessorsToUse As System.Windows.Controls.Label
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",44)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents slNumRecords As System.Windows.Controls.Slider
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",45)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents lblSize As System.Windows.Controls.Label
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",46)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents lblLinqTime As System.Windows.Controls.Label
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",47)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents lblPlinqTime As System.Windows.Controls.Label
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",48)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents graphLinq As BabyNames.Graph
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\MainWindow.xaml",53)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents graphPlinq As BabyNames.Graph
        
        #End ExternalSource
        
        Private _contentLoaded As Boolean
        
        '''<summary>
        '''InitializeComponent
        '''</summary>
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
            If _contentLoaded Then
                Return
            End If
            _contentLoaded = true
            Dim resourceLocater As System.Uri = New System.Uri("/BabyNames;component/mainwindow.xaml", System.UriKind.Relative)
            
            #ExternalSource("..\..\MainWindow.xaml",1)
            System.Windows.Application.LoadComponent(Me, resourceLocater)
            
            #End ExternalSource
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Function _CreateDelegate(ByVal delegateType As System.Type, ByVal handler As String) As System.[Delegate]
            Return System.[Delegate].CreateDelegate(delegateType, Me, handler)
        End Function
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
         System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes"),  _
         System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"),  _
         System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")>  _
        Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
            If (connectionId = 1) Then
                Me.MainWindow = CType(target,BabyNames.Window1)
                
                #ExternalSource("..\..\MainWindow.xaml",5)
                AddHandler Me.MainWindow.Loaded, New System.Windows.RoutedEventHandler(AddressOf Me.MainWindow_Loaded)
                
                #End ExternalSource
                Return
            End If
            If (connectionId = 2) Then
                Me.MainGrid = CType(target,System.Windows.Controls.Grid)
                Return
            End If
            If (connectionId = 3) Then
                Me.btnRunLinq = CType(target,System.Windows.Controls.Button)
                
                #ExternalSource("..\..\MainWindow.xaml",25)
                AddHandler Me.btnRunLinq.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnRunLinq_Click)
                
                #End ExternalSource
                Return
            End If
            If (connectionId = 4) Then
                Me.btnRunPlinq = CType(target,System.Windows.Controls.Button)
                
                #ExternalSource("..\..\MainWindow.xaml",29)
                AddHandler Me.btnRunPlinq.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnRunPlinq_Click)
                
                #End ExternalSource
                Return
            End If
            If (connectionId = 5) Then
                Me.lblSpeedup = CType(target,System.Windows.Controls.Label)
                Return
            End If
            If (connectionId = 6) Then
                Me.txtQueryName = CType(target,System.Windows.Controls.TextBox)
                Return
            End If
            If (connectionId = 7) Then
                Me.lblQueryName = CType(target,System.Windows.Controls.Label)
                Return
            End If
            If (connectionId = 8) Then
                Me.txtQueryState = CType(target,System.Windows.Controls.TextBox)
                Return
            End If
            If (connectionId = 9) Then
                Me.lblQueryState = CType(target,System.Windows.Controls.Label)
                Return
            End If
            If (connectionId = 10) Then
                Me.slProcessorsToUse = CType(target,System.Windows.Controls.Slider)
                
                #ExternalSource("..\..\MainWindow.xaml",42)
                AddHandler Me.slProcessorsToUse.ValueChanged, New System.Windows.RoutedPropertyChangedEventHandler(Of Double)(AddressOf Me.slProcessorsToUse_ValueChanged)
                
                #End ExternalSource
                Return
            End If
            If (connectionId = 11) Then
                Me.lblProcessorsToUse = CType(target,System.Windows.Controls.Label)
                Return
            End If
            If (connectionId = 12) Then
                Me.slNumRecords = CType(target,System.Windows.Controls.Slider)
                
                #ExternalSource("..\..\MainWindow.xaml",44)
                AddHandler Me.slNumRecords.ValueChanged, New System.Windows.RoutedPropertyChangedEventHandler(Of Double)(AddressOf Me.slSize_ValueChanged)
                
                #End ExternalSource
                Return
            End If
            If (connectionId = 13) Then
                Me.lblSize = CType(target,System.Windows.Controls.Label)
                Return
            End If
            If (connectionId = 14) Then
                Me.lblLinqTime = CType(target,System.Windows.Controls.Label)
                Return
            End If
            If (connectionId = 15) Then
                Me.lblPlinqTime = CType(target,System.Windows.Controls.Label)
                Return
            End If
            If (connectionId = 16) Then
                Me.graphLinq = CType(target,BabyNames.Graph)
                Return
            End If
            If (connectionId = 17) Then
                Me.graphPlinq = CType(target,BabyNames.Graph)
                Return
            End If
            Me._contentLoaded = true
        End Sub
    End Class
End Namespace

