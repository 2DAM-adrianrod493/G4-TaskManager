   M S B A M L   `   `   `  ����N  JG4-EmployeeRegister, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:G4_EmployeeRegister.ViewsG4_EmployeeRegister.Views  5      S.clr-namespace:G4_EmployeeRegister.UserControls G4_EmployeeRegister.UserControls  5      /    )G4_EmployeeRegister.Views.VerFichajesPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:G4_EmployeeRegister.Views     5      =uc.clr-namespace:G4_EmployeeRegister.UserControls     5        N� Title$  VerFichajesPage��5
       N� 
Background 5      �� 5   
    �� ImageSource$# ../Resources/Fondos/fondo.jpg��6    j� Stretch$
 Fill=�6A    �� Opacity$	 0.5Q�6P   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��5* �6   .+�5   
   �  � ColumnDefinitions 5      �� 5      $��4* �6#   �� 5      $��* �6#   .+�5"      �� -   5         ��Name$ 
BotonSalir��5"      $��2��5#      $��Center=�5$      
������ $��0q�5&      .��5'      �� $!��/Resources/Iconos/salir.png��6   $��38��5(         EstiloImagen#��[  5)      5*      5+      � 5.   
   $��1��6   .+�5/      �� $#��Adrián Rodríguez Rodríguez��6   $��1��50      $��1��51      $��Center=�52      $��Center=�53        EstiloTextoTitulo#��[ 54      55      �� 58   
   $��2��6   .+�59      � .+�5:      �� $
��Auto=�6   $��Disabled=�6B   .��5;      �� .+�5<      &   System.Windows.Controls.DataGrid  -   5          Name$ HistorialFichaje��5<   #   	   AutoGenerateColumns$	 False��5=      $��Stretch=�5>      $��Stretch=�5?      
   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 * 5@         
IsReadOnly$
 True��5A         CanUserAddRows$ False��5C         CanUserDeleteRows$ False��5D        EstilosDataGrid#��[ 5E      ��5B      �� *
Fichajes+   Columns 5G      0  *System.Windows.Controls.DataGridTextColumn  5H   "   ,  &System.Windows.Controls.DataGridColumn   Header$
 Foto��65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *Foto+  5I   "   $ 	FechaHora��65    6H   �� *	FechaHora+  5J   "   $
 Tipo��65    6C   �� *Tipo+  5K   "   $ Observaciones��65    6L   �� *Observaciones+5N      5O      5P      5Q      5R      5S      5U      