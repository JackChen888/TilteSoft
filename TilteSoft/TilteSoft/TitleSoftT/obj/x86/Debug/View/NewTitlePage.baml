   M S B A M L   `   `   `  ����E  ATitleSoftT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:TitleSoftT.ViewTitleSoftT.View  5   	   T PCefSharp.Wpf, Version=67.0.0.0, Culture=neutral, PublicKeyToken=40c4b6fc221f4138A0clr-namespace:CefSharp.Wpf;assembly=CefSharp.WpfCefSharp.Wpf 5   	   9!clr-namespace:TitleSoftT.ConvertsTitleSoftT.Converts  5   	   "    TitleSoftT.View.NewTitlePage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:TitleSoftT.View     5   	   @cef0clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf     5   	   6converts!clr-namespace:TitleSoftT.Converts     5   	   -   5          � Title$  NewTitlePage��5
   	   $	��768��6   $
��1280��6+    � 	Resources 5      %   TitleSoftT.Converts.IntToString   IntToString��%   &	          5   
   65   5      .��5      �  � RowDefinitions 5   
   �� 5      $	��100 �6   6+   �� 5      $��80 �6   6*   �� 5      $��0 �6   6)   �� 5      6   .+�5   
   � -   5        $��
WrapPanels��5   /   $��0��6   $	��180��6"   .+�5      �� $
��5,20q�6   
�������$��20(�6N   ��6:   �� *No+6^   �� 5      -   5        $��AndPic��5   <   $��10,10q�6   $��80��6%   $��80��60   $��20(�6`     
MyButStyle#���  6n   .��6�   	未完成6�   �� 5      -   5        $��SelectPicList��5   <   $��10,10q�6   $��80��6%   $��80��60   $��20(�6R   #���  6|   .��6�   	已完成6�   �� 5      $��20��6   
���  ����6$   �� *
SavaPicMsg+6T   5      �� 5   
   -   5         ��Name$ hello1��5      $��1��6%    �� Orientation$ 
Horizontal=�62   $��Center=�6K   $��Center=�6f   
������� J� 	AllowDrop . .+�5      �� $ 
Horizontal=�6   .+�5      �� ��6   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �� �� Path$ txtUserName�5      �� 5       -   5        	 ��Name$	 txtKeyWorlds��5       $	��200��6,   $��0,0,10,0q�68   $��24(�6e   ��6J   �� *	TitleName+6u   �� 5!      -   5        
 ��Name$
 	btnSearch��5!      $	��100��6)   $��30��65   #���  6A   $��搜索��6f   $��0,0,10,0q�6�   6�   �� 5"      -   5        $
 
btnPrePage��5"      $	��100��6)   $��30��65   $��	上一页��6A   #���  6O   $y�	Collapsed=�6�   6�   �� 5#      -	   5        $
 btnNextPage��5#      $	��100��6*   $��30��66   $��	下一页��6B   #���  6P   $��0,0,10,0q�6�   6�   �� 5$      -
   5        $
 btnSaveData��5$      $	��100��6*   $��30��66   $��保存��6B   #���  6O   $��0,0,10,0q�6�   6�   �� 5&      -   5         ��Name$ 	labCounts��5&      $��已选素材 0 张��6'   
���  ��$��0,0,10,0q�6K   6_   �� 5'      
���  ��$��0,0,10,0q�6=   ��6   �� *Msg+6Q   �� 5(      -   5        $��TitleHandle��5(   `   $	��100��6   $��30��6"   $��隐藏��6.   #���  6;   $��0,0,10,0q�6�   6�   5)      �� 5+   
   -   5        $ hello2��5+      $��1��6%   $y�	Collapsed=�62   $ 
Horizontal=�6I   $��Center=�6b   $��Center=�6}   
������� . .+�5,      �� $ 
Horizontal=�6   .+�5-      �� ��6   ��  �� *FindAncestor+" ��$ txtUserName�5.      �� 5/      -   5        $	 txtKeyWorlds2��5/      $	��200��6-   $��0,0,10,0q�69   6M   �� 50      -   5        $
 
btnSearch2��50      $	��100��6*   $��30��66   $��搜索��6B   $��0,0,10,0q�6g   6{   �� 51      -   5        $
 btnPrePage2��51      $	��100��6*   $��30��66   $��	上一页��6B   $y�	Collapsed=�6i   6�   �� 52      -   5        $
 btnNextPage2��52      $	��100��6+   $��30��67   $��	下一页��6C   $��0,0,10,0q�6k   6   �� 53      -   5        $
 btnSaveData2��53      $	��100��6+   $��30��67   $��提交数据入库��6C   $��0,0,10,0q�6n   6�   54      %  CefSharp.Wpf.ChromiumWebBrowser  55   
   -   5          Name$ cefWebBrowser��55   .   $��2��6!   6G   � 56   
   -   5        $��DisplayPage1��56      $��3��6   $y�Visible=�60    57      �� 58      $��50 �6    6.   �� 59      6"   .+�5;      �� $��0��6   $ 
Horizontal=�6&   .+�5<      �� -   5        $ txtKeys��5<      $��
关键字 ��6)   $��24(�68   6I   � 5=      -   5         �Name$ warpPanelKeys��5=      65   5>      �� 5?      -   5         ��Name$ AddScrollViewer��5?   )   $��1��6   $��Visible=�6B   $
��1920��6h   .��5@      � -   5        $ AddStackPanel��5@      $��1��63    � Orientation$ 
Horizontal=�6@   $	��Top=�6Y   $��Center=�6q   5A      5B      5C      �� 5E   
   -   5        $��PicStackPanel��5E   "   $��3��6   $ 
Horizontal=�67   $	��500��6P   $y�	Collapsed=�6]   .+�5F      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5F   �   $	��480��6   $	��800��6$   $��40,10,10,10q�6�      AutoGenerateColumns .  ��60   �� *AddTitlePageViewModel.Items+ �� SelectedItem 6e   �� *#!AddTitlePageViewModel.SelectItems+   ColumnHeaderStyle 5G      �� 5H       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5I      ��  �� Property � �� �� Value$ SimHei)�67   �� 5J       � �� �#104E8B67   �� 5K       � ��$ 20(�65   �� 5L       � ��$
 Auto��63   �� 5M       � ��$ 1q�6<   �� 5N       � ��$ Center=�6G   5O         Columns 5Q      0  *System.Windows.Controls.DataGridTextColumn  5R      ,  &System.Windows.Controls.DataGridColumn   Header$ id��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *ID+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6     5S      $ 	旧标签��6*   $	 100 6�    67   �� *
OldTitle+$ TwoWay=�$ PropertyChanged=�6�     5T      $ 	新标签��6*   $	 100 6�    67   �� *
NewTitle+$ TwoWay=�$ PropertyChanged=�6�     5U      $ 录入时间��6*   $	 100 6�    68   �� *UpTime+$ TwoWay=�$ PropertyChanged=�6�     5V      $ 是否审批��6*   $	 100 6�    68   �� *IsAdd+ �� 	Converter# [  $ TwoWay=�$ PropertyChanged=�6�   4	  .System.Windows.Controls.DataGridTemplateColumn	  5W       	  CellTemplate 5X      �� 5Y      .R�5Z   "   �� $��AddTitle��6b   -   5        ��5Z   +   �� *BaiDuSearch+.��6r   查看6v   5\      5^      5j      5k      5l      5m      