   M S B A M L   `   `   `  ����N  JWarehouseAppManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WarehouseAppManager.ViewWarehouseAppManager.View  5   	   M+clr-namespace:WarehouseAppManager.ViewModelWarehouseAppManager.ViewModel  5	   	   C&clr-namespace:WarehouseAppManager.ViewWarehouseAppManager.View  5
   	   (    "WarehouseAppManager.View.AddWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   2local&clr-namespace:WarehouseAppManager.View  5   	   4vm+clr-namespace:WarehouseAppManager.ViewModel  5	   	   .v&clr-namespace:WarehouseAppManager.View  5
   	   -   5          �Name$  
add_window��5   	    � Title$ 	AddWindow��5   	   $	��556��6   $	��450��6(    � WindowStartupLocation$ CenterScreen=�5   	   $��1q�5   	   $��20(�5   	    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5      3  -WarehouseAppManager.ViewModel.AddingViewModel   addingVM1  +WarehouseAppManager.ViewModel.MainViewModel 
 mainVM-  'WarehouseAppManager.ViewModel.Converter  myConverter��%W   &	        &	      &	 &       5   
   6     5   
   6     5   
   6   5      .��5      � $��5q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$      �� 5%      .+�5(   
   �� $��0��6   $��0��6   $��2��6-   $��Center=�5)      $��Center=�5*      ��5+      �� *AddingVM.Magazyn.Nazwa+�� 5-   
   $��1��6   $��0��6   $��Center=�5.      $��5q�5/      $��Nazwa��50      �� 51   
   $��1��6   $��1��6     �� SelectedIndex$ 0��52      $��Center=�53      $��Center=�54      $��5q�55       �� 
IsEditable . 	 �� Text	 57      �� *AddingVM.NazwaTowaru+
 �� Mode$
 TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=���58      �� *AddingVM.Towary+ �� SelectedItem 59      �� *AddingVM.BiezacyTowar+��5:      �� 5;      .R�5<      �� ��5=      `� 5>       �� StringFormat$ 	{0} ({1})��6+   .1�5?   "   ��  �� Path$ Nazwa�6*   �� 5@   "   $ Regal�6*   5A      5C      5D      .  (System.Windows.Interactivity.Interaction   Triggers 5F      /  )System.Windows.Interactivity.EventTrigger  5G         	EventName$ SelectionChanged��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5H      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� *AddingVM.UzupelnijFormularz+5I      5K      �� 5M   
   $��2��6   $��0��6   $��Center=�5N      $��5q�5O      $��Ilość��5P      ,   &WarehouseAppManager.View.NumberTextBox  5Q   
   $��2��5R      $��1��6   $��25(�5S      $��Stretch=�5T      $��5q�5U         Text 5V      �� $ AddingVM.Ilosc�$
 TwoWay=�$ PropertyChanged=� �� 	Converter# [ 5Y      �� 5[   
   $��3��6   $��0��6   $��Center=�5\      $��5q�5]      $��	Jednostka��5^      �� 5_   
   $��3��6   $��1��6   $��Center=�5`      $��Center=�5a      $��5q�5b      {�5c      �� *AddingVM.IsNotInDB+��5d      �� *AddingVM.Jednostka+$
 TwoWay=�$ PropertyChanged=��� 5g   
   $��4��6   $��0��6   $��Center=�5h      $��5q�5i      $��Regał��5j      �� 5k   
   $��4��6   $��1��6   $��Center=�5l      $��Center=�5m      $��5q�5n      ��5o      �� *AddingVM.Regal+$
 TwoWay=�$ PropertyChanged=��� 5r   
   $��5��6   $��0��6   $��Center=�5s      $��5q�5t      $��Id Pracownika��5u        5v   
   $��5��5w      $��1��6   $��25(�5x      $��Stretch=�5y      $��5q�5z       5{      �� $ AddingVM.IdPracownika�$
 TwoWay=�$ PropertyChanged=�# [ 5~      �� 5�   
   $��6��6   $��1��6   $��Center=�5�      $��Center=�5�      $��30(�5�      $��5q�5�      $��Dodaj��5�      {�5�      �� *AddingVM.MoznaDodacTowar+��5�      �� *AddingVM.DodajTowarDoBazy+��5�      ��  �� ElementName$ 
add_window��5�      5�      5�      