   M S B A M L   `   `   `  ����   XisoForgeGUI"    XisoForgeGUI.View.MainWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  XisoForgeGUI��$	��500��$	��750�� � WindowStyle$
 None=� � 
ResizeMode$ 	CanResize=� � WindowStartupLocation$ CenterScreen=� � AllowsTransparency$
 True��
������ $��0q� � Icon$ /View/XisoForgeIMG.png��'  !System.Windows.Shell.WindowChrome   WindowChrome      CaptionHeight$ 30Q�   GlassFrameThickness$ 0q�	   ResizeBorderThickness$	 5q�
 � 	Resources
  �� 
TargetType �� Property �� Value �� 
TargetType d�Name �� 	ClickMode d� 	IsChecked �� Mode �� RelativeSource ��Name � ColumnDefinitions F�Name �� Triggers P� Property P� Value �� 
TargetName ��Name �� SelectionBoxItem �� SelectionBoxItemTemplate# �� SelectionBoxItemStringFormat �� ContentStringFormat  J� IsHitTestVisible! �Name" �� IsDropDownOpen# � AllowsTransparency$ � ActualWidth% �� ElementName& J� SnapsToDevicePixels' K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation(   DirectionalNavigation) �� Padding* �� CornerRadius+ ��Name��%N
  '��       '�� *    '�� O    '�� ,	    �� " ��.���  � ��
 �������  � �� �#333333��  � �� �#555555��  � ��$ 1q���  � ��$	 8,6q���  � ��$ 14(���  � ��$ 30����  �@J�SnapsToDevicePixels$
 True����  � �� �� " ��.R�� .+�d� $ ToggleButton��$ Press=�#��z��#��z��#��z��$|�False�� �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+���� " d�.R��� $ Border��#��z��#��z��#��z��._��  �� $��* ��� $
��Auto �.+��� $��0��#��z��#��z��$
��Left=�$��Center=�$��5,0,0,0q�F� $ Arrow��$��1��"����        � z  z � $�     	
�������$��Center=�$��Center=�$��0,0,8,0q� P�  �@J�IsMouseOver$
 True��. ��� $ Border�� � �� �#444444P�  �@d�	IsChecked$
 TrueY�. ��� $ Border�� � �� �#444444�� $ ContentPresenter��#��z #��z # z $
��Left=�$��Center=�$��10,0,0,0q�$  False��� $! 
PART_Popup��$��Bottom=�#��z" $
# True��$|�False��$��Slide=�#��z$ .���� $ DropDownBorder�����#333333���#555555$��1q����� *ActualWidth+$% ToggleButton��._��� $
& True��$	��250��.���� $
' True��$( 	Contained=� P�  � {�$ False��. ���  � �� �#888888P�  �@��
IsGrouping$
 True��. ���  � ��$ False���� " ��.���  �@J�SnapsToDevicePixels$
 True����  � ��
 �������  � ��$	 8,6q���  � ��$ 30����  � ��$ Stretch=���  � ��$ Center=���  � ��$ 14(���  � �� �� " ��.R��� $ Border��
������ $��0q�#) z��#��z��._��� #��z��#��z�� P�  �@J�IsMouseOver$
 True��. ��� $ Border�� � �� �#555555P�  �@��IsHighlighted$
 True��. ��� $ Border�� � �� �#555555P�  �@��
IsSelected$
 True��. ��� $ Border�� � �� �#3CB371�� " ��.���  � �� �#444444��  � ��
 �������  � �� �#555555��  � ��$ 1q���  � ��$ 5q���  � �� �� " ��.R��� $ Border��#��z��#��z��#��z��$* 3��._��� $��Center=�$��Center=�#��z�� P�  �@J�IsMouseOver$
 True��. ��� $ Border�� � �� �#555555P�  � ��$
 True��. ��� $ Border�� � �� �#666666P�  � {�$ False��. ��� $ Border�� �@J�Opacity$	 0.5Q��� " ��.���  � �� �#333333��  � ��
 �������  � �� �#555555��  � ��$ 1q���  � ��$ 5q���  � �� �� " ��.R��� #��z��#��z��#��z��$* 3��._��� $+ PART_ContentHost��#��z��$��Center=�.���� $* 5�����#272727���#4b4b4b$��1q�._�� , � RowDefinitions, �� $��30 ��� $��* �.+�� $��0�����#3CB371 �� $��* ��� $��30 �.+��� $��XisoForgeGUI��
�������$��14(�$��SemiBold"�$��Center=�$��Center=��� -   - ��Name$- BtnClose��$��1��$	��✕��
������ $��0q�
�������$��12(�.   IsHitTestVisibleInChrome$
. True��� $��1��$��20q�, �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+�� $��0��$��0,0,0,15q� �� $	��100 ��� $��* �.+��� $��Mode:��
�������$��14(�$��Center=��� -   / ��Name$/ CmbMode��$��1��.@��� $3��-📂 Extract => XISO contents to a directory.��
0 � Tag$0 Extract��1 �� 
IsSelected$
1 True���� $/��)📦 Create =>  an XISO from a directory.��$0 Create���� $9��3📄 List =>  the file contents within an XISO file��$
0 List���� $9��3🔄 Rewrites =>  file system structure of an XISO.��$0 Rewrite��� $��1��$��0,0,0,15q� �� $	��100 ��� $��* ��� $	��120 �.+��� $��	File ISO:��
�������$��14(�$��Center=��� -   2 ��Name$2 
TxtIsoPath��$��1��$��0,0,10,0q�3 �� 
IsReadOnly$
3 True���� -   $- BtnChooseIso��$��2��$��	Browse...�����#444444� $��2��$��0,0,0,15q� �� $	��100 ��� $��* ��� $	��120 �.+��� $��Destination:��
�������$��14(�$��Center=��� -   $2 TxtOutputDir��$��1��$��0,0,10,0q�$
3 True���� -   $- BtnChooseOutput��$��2��$��	Browse...��� $��3��$��0,0,0,15q�, �� $
��Auto ��� $��* �.+��� $��0��$��Execution log:��
�������$��14(�$��0,0,0,5q��� $��1�����#1A1A1A���#555555$��1q�$* 3��._��� -   4 ��Name$4 TxtLog��$
3 True��"5 �� VerticalScrollBarVisibility$
5 Auto=�
������ ���#CCCCCC$��0q�$��Consolas)�� $��4��.+��� 6 �� Orientation$6 
Horizontal=�$��Right=�.+��� -   $- BtnRun��$	��Run��$	��120��$��30��$��0,0,10,0q����#3CB371���#2E8B57�� -	   $- 	BtnCancel��$��Cancel��$	��120��$��30�����#555555���#444444${�False��