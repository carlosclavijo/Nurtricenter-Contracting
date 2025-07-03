˚
qC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Shared\PhoneNumberValue.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Shared #
;# $
public 
record 
PhoneNumberValue 
{ 
private 
static 
readonly 
Regex !

PhoneRegex" ,
=- .
new/ 2
(2 3
$str3 A
,A B
RegexOptionsC O
.O P
CompiledP X
)X Y
;Y Z
public		 

string		 
Phone		 
{		 
get		 
;		 
init		 #
;		# $
}		% &
public 

PhoneNumberValue 
( 
string "
phone# (
)( )
{ 
if 

( 
string 
. 
IsNullOrEmpty  
(  !
phone! &
)& '
)' (
{ 	
throw 
new !
ArgumentNullException +
(+ ,
$str, J
,J K
nameofL R
(R S
phoneS X
)X Y
)Y Z
;Z [
} 	
if 

( 
! 

PhoneRegex 
. 
IsMatch 
(  
phone  %
)% &
)& '
{ 	
throw 
new 
ArgumentException '
(' (
$str( m
,m n
nameofo u
(u v
phonev {
){ |
)| }
;} ~
} 	
Phone 
= 
phone 
; 
} 
public 

static 
implicit 
operator #
string$ *
(* +
PhoneNumberValue+ ;
phone< A
)A B
{ 
return 
phone 
== 
null 
? 
$str !
:" #
phone$ )
.) *
Phone* /
;/ 0
} 
public 

static 
implicit 
operator #
PhoneNumberValue$ 4
(4 5
string5 ;
phone< A
)A B
{ 
return   
new   
PhoneNumberValue   #
(  # $
phone  $ )
)  ) *
;  * +
}!! 
}## ô
nC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Shared\FullNameValue.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Shared #
;# $
public 
record 
FullNameValue 
{ 
private 
static 
readonly 
Regex !
	NameRegex" +
=, -
new. 1
(1 2
$str2 E
,E F
RegexOptionsG S
.S T
CompiledT \
)\ ]
;] ^
private		 
const		 
int		 
	MaxLength		 
=		  !
$num		" %
;		% &
public 

string 
Name 
{ 
get 
; 
init "
;" #
}$ %
public 

FullNameValue 
( 
string 
name  $
)$ %
{ 
if 

( 
string 
. 
IsNullOrEmpty  
(  !
name! %
)% &
)& '
{ 	
throw 
new !
ArgumentNullException +
(+ ,
$str, G
,G H
nameofI O
(O P
nameP T
)T U
)U V
;V W
} 	
if 

( 
name 
. 
Length 
> 
	MaxLength #
)# $
{ 	
throw 
new 
ArgumentException '
(' (
$"( *
$str* B
{B C
	MaxLengthC L
}L M
$strM X
"X Y
,Y Z
nameof[ a
(a b
nameb f
)f g
)g h
;h i
} 	
if 

( 
! 
	NameRegex 
. 
IsMatch 
( 
name #
)# $
)$ %
{ 	
throw 
new 
ArgumentException '
(' (
$str( W
,W X
nameofY _
(_ `
name` d
)d e
)e f
;f g
} 	
Name 
= 
NormalizeName 
( 
name !
)! "
;" #
} 
public!! 

static!! 
implicit!! 
operator!! #
string!!$ *
(!!* +
FullNameValue!!+ 8
name!!9 =
)!!= >
{"" 
return## 
name## 
==## 
null## 
?## 
$str##  
:##! "
name### '
.##' (
Name##( ,
;##, -
}$$ 
public&& 

static&& 
implicit&& 
operator&& #
FullNameValue&&$ 1
(&&1 2
string&&2 8
name&&9 =
)&&= >
{'' 
return(( 
new(( 
FullNameValue((  
(((  !
name((! %
)((% &
;((& '
})) 
public++ 

string++ 
NormalizeName++ 
(++  
string++  &
name++' +
)+++ ,
{,, 
name-- 
=-- 
Regex-- 
.-- 
Replace-- 
(-- 
name-- !
.--! "
Trim--" &
(--& '
)--' (
,--( )
$str--* 0
,--0 1
$str--2 5
)--5 6
;--6 7
name// 
=// 
CultureInfo// 
.// 
CurrentCulture// )
.//) *
TextInfo//* 2
.//2 3
ToTitleCase//3 >
(//> ?
name//? C
.//C D
ToLower//D K
(//K L
)//L M
)//M N
;//N O
return11 
name11 
;11 
}22 
}33 √
jC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Shared\CostValue.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Shared #
;# $
public 
record 
	CostValue 
{ 
public 

decimal 
Value 
{ 
get 
; 
init  $
;$ %
}& '
public 

	CostValue 
( 
decimal 
value "
)" #
{ 
if		 

(		 
value		 
<		 
$num		 
)		 
{

 	
throw 
new 
ArgumentException '
(' (
$str( G
,G H
nameofI O
(O P
valueP U
)U V
)V W
;W X
} 	
else 
if 
( 
value 
== 
null 
) 
{ 	
throw 
new !
ArgumentNullException +
(+ ,
$str, G
,G H
nameofI O
(O P
valueP U
)U V
)V W
;W X
} 	
Value 
= 
value 
; 
} 
public 

static 
implicit 
operator #
decimal$ +
(+ ,
	CostValue, 5
cost6 :
): ;
{ 
return 
cost 
== 
null 
? 
$num 
:  !
cost" &
.& '
Value' ,
;, -
} 
public 

static 
implicit 
operator #
	CostValue$ -
(- .
decimal. 5
a6 7
)7 8
{ 
return 
new 
	CostValue 
( 
a 
) 
;  
} 
} “
qC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Patients\PatientFactory.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Patients %
;% &
public 
class 
PatientFactory 
: 
IPatienteFactory .
{ 
public 

Patient 
Create 
( 
string  
patientName! ,
,, -
string. 4
patientPhone5 A
)A B
{ 
if 

( 
string 
. 
IsNullOrWhiteSpace %
(% &
patientName& 1
)1 2
)2 3
{ 	
throw		 
new		 
ArgumentException		 '
(		' (
$str		( B
,		B C
nameof		D J
(		J K
patientName		K V
)		V W
)		W X
;		X Y
}

 	
if 

( 
string 
. 
IsNullOrWhiteSpace %
(% &
patientPhone& 2
)2 3
)3 4
{ 	
throw 
new 
ArgumentException '
(' (
$str( C
,C D
nameofE K
(K L
patientPhoneL X
)X Y
)Y Z
;Z [
} 	
return 
new 
Patient 
( 
patientName &
,& '
patientPhone( 4
)4 5
;5 6
} 
} ÿ
jC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Patients\Patient.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Patients %
;% &
public 
class 
Patient 
: 
AggregateRoot $
{ 
public		 

FullNameValue		 
Name		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
public

 

PhoneNumberValue

 
Phone

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
public 

Patient 
( 
string 
name 
, 
string  &
phone' ,
), -
:. /
base0 4
(4 5
Guid5 9
.9 :
NewGuid: A
(A B
)B C
)C D
{ 
Name 
= 
name 
; 
Phone 
= 
phone 
; 
AddDomainEvent 
( 
new 
PatientCreated #
(# $
Id$ &
,& '
Name( ,
,, -
Phone. 3
)3 4
)4 5
;5 6
} 
private 
Patient 
( 
) 
{ 
} 
} ∂
uC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Patients\IPatientRepository.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Patients %
;% &
public 
	interface 
IPatientRepository #
:$ %
IRepository& 1
<1 2
Patient2 9
>9 :
{ 
Task 
UpdateAsync	 
( 
Patient 
patient $
)$ %
;% &
Task 
DeleteAsync	 
( 
Guid 
id 
) 
; 
}

 ö
rC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Patients\IPatientFactory.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Patients %
;% &
public 
	interface 
IPatienteFactory !
{ 
Patient 
Create 
( 
string 
patientName %
,% &
string' -
patientPhone. :
): ;
;; <
} Õ
xC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Patients\Events\PatientCreated.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Patients %
.% &
Events& ,
;, -
public 
record 
PatientCreated 
( 
Guid !
	PatientId" +
,+ ,
string- 3
Name4 8
,8 9
string: @
PhoneA F
)F G
:H I
DomainEventJ U
;U VÆ
ÄC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\DeliveryDays\Events\DeliveryDayUpdated.cs
	namespace 	
Contracting
 
. 
Domain 
. 
DeliveryDays )
.) *
Events* 0
;0 1
public 
record 
class 
DeliveryDayUpdated &
(& '
Guid' +

ContractId, 6
,6 7
Guid8 <
DeliveryDayId= J
,J K
stringL R
StreetS Y
,Y Z
int[ ^
Number_ e
)e f
:g h
DomainEventi t
;t u∞
ÄC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\DeliveryDays\Events\DeliveryDayDeleted.cs
	namespace 	
Contracting
 
. 
Domain 
. 
DeliveryDays )
.) *
Events* 0
;0 1
public 
record 
DeliveryDayDeleted  
(  !
Guid! %

ContractId& 0
,0 1
Guid2 6
DeliveryDayId7 D
)D E
:F G
DomainEventH S
;S T›
rC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\DeliveryDays\DeliveryDay.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Delivery %
;% &
public 
class 
DeliveryDay 
: 
Entity !
{ 
public 

Guid 

ContractId 
{ 
get  
;  !
set" %
;% &
}' (
private		 
DateTime		 
_date		 
;		 
public

 

DateTime

 
Date

 
{ 
get 
=> 
_date 
; 
set 
=> 
_date 
= 
value 
. 
Kind !
==" $
DateTimeKind% 1
.1 2
Utc2 5
?6 7
value8 =
:> ?
value@ E
.E F
ToUniversalTimeF U
(U V
)V W
;W X
} 
public 

string 
Street 
{ 
get 
; 
private  '
set( +
;+ ,
}- .
public 

int 
Number 
{ 
get 
; 
private $
set% (
;( )
}* +
public 

DeliveryDay 
( 
Guid 

contractId &
,& '
DateTime( 0
date1 5
,5 6
string7 =
street> D
,D E
intF I
numberJ P
)P Q
:R S
baseT X
(X Y
GuidY ]
.] ^
NewGuid^ e
(e f
)f g
)g h
{ 

ContractId 
= 

contractId 
;  
Date 
= 
date 
. 
Kind 
== 
DateTimeKind (
.( )
Unspecified) 4
?5 6
DateTime7 ?
.? @
SpecifyKind@ K
(K L
dateL P
,P Q
DateTimeKindR ^
.^ _
Utc_ b
)b c
:d e
datef j
.j k
ToUniversalTimek z
(z {
){ |
;| }
Street 
= 
street 
; 
Number 
= 
number 
; 
} 
public 

void 
Update 
( 
string 
street $
,$ %
int& )
number* 0
)0 1
{ 
Street 
= 
street 
; 
Number 
= 
number 
; 
AddDomainEvent 
( 
new 
DeliveryDayUpdated '
(' (

ContractId( 2
,2 3
Id4 6
,6 7
street8 >
,> ?
number@ F
)F G
)G H
;H I
}   
public"" 
void"" 
Delete"" 
("" 
)"" 
{## 
AddDomainEvent$$ 
($$ 
new$$ 
DeliveryDayDeleted$$ '
($$' (

ContractId$$( 2
,$$2 3
Id$$4 6
)$$6 7
)$$7 8
;$$8 9
}%% 
private'' 
DeliveryDay'' 
('' 
)'' 
{'' 
}'' 
}(( Ω
wC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Contracts\IContractRepository.cs
	namespace 	
Contracting
 
. 
Domain 
. 
	Contracts &
;& '
public 
	interface 
IContractRepository $
:% &
IRepository' 2
<2 3
Contract3 ;
>; <
{ 
Task 
UpdateAsync	 
( 
Contract 
contract &
)& '
;' (
Task 
DeleteAsync	 
( 
Guid 
id 
) 
; 
}		 Û
tC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Contracts\IContractFactory.cs
	namespace 	
Contracting
 
. 
Domain 
. 
	Contracts &
;& '
public 
	interface 
IContractFactory !
{ 
Contract #
CreateHalfMonthContract $
($ %
Guid% )
administratorId* 9
,9 :
Guid; ?
	patientId@ I
,I J
DateTimeK S
	startDateT ]
)] ^
;^ _
Contract #
CreateFullMonthContract $
($ %
Guid% )
administratorId* 9
,9 :
Guid; ?
	patientId@ I
,I J
DateTimeK S
	startDateT ]
)] ^
;^ _
} ‘
àC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Contracts\Exceptions\ContractCreationException.cs
	namespace 	
Contracting
 
. 
Domain 
. 
	Contracts &
.& '

Exceptions' 1
;1 2
public 
class %
ContractCreationException &
(& '
string' -
message. 5
)5 6
:7 8
	Exception9 B
(B C
$strC m
+n o
messagep w
)w x
;x y∂
rC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Contracts\ContractStatus.cs
	namespace 	
Contracting
 
. 
Domain 
. 
	Contracts &
;& '
public 
enum 
ContractStatus 
{ 
Created 
, 

InProgress 
, 
	Completed 
} ç
pC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Contracts\ContractType.cs
	namespace 	
Contracting
 
. 
Domain 
. 
	Contracts &
;& '
public 
enum 
ContractType 
{ 
	HalfMonth 
, 
	FullMonth 
} é
yC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Contracts\Events\CreateCalendar.cs
	namespace 	
Contracting
 
. 
Domain 
. 
	Contracts &
.& '
Events' -
;- .
public 
record 
CreateCalendar 
( 
Guid !

ContractId" ,
,, -
Guid. 2
	PatientId3 <
,< =
DateTime> F
	StartDateG P
,P Q
DateTimeR Z
EndDate[ b
,b c
Listd h
<h i
DeliveryDayi t
>t u
DeliveryDays	v Ç
)
Ç É
:
Ñ Ö
DomainEvent
Ü ë
;
ë íÃ
sC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Contracts\ContractFactory.cs
	namespace 	
Contracting
 
. 
Domain 
. 
	Contracts &
;& '
public 
class 
ContractFactory 
: 
IContractFactory /
{ 
public 

Contract #
CreateFullMonthContract +
(+ ,
Guid, 0
administratorId1 @
,@ A
GuidB F
	patientIdG P
,P Q
DateTimeR Z
	startDate[ d
)d e
{ 
if 

( 
administratorId 
== 
Guid #
.# $
Empty$ )
)) *
{ 	
throw		 
new		 !
ArgumentNullException		 +
(		+ ,
$str		, I
,		I J
nameof		K Q
(		Q R
administratorId		R a
)		a b
)		b c
;		c d
}

 	
if 

( 
	patientId 
== 
Guid 
. 
Empty #
)# $
{ 	
throw 
new !
ArgumentNullException +
(+ ,
$str, C
,C D
nameofE K
(K L
	patientIdL U
)U V
)V W
;W X
} 	
return 
new 
Contract 
( 
administratorId +
,+ ,
	patientId- 6
,6 7
ContractType8 D
.D E
	FullMonthE N
,N O
	startDateP Y
)Y Z
;Z [
} 
public 

Contract #
CreateHalfMonthContract +
(+ ,
Guid, 0
administratorId1 @
,@ A
GuidB F
	patientIdG P
,P Q
DateTimeR Z
	startDate[ d
)d e
{ 
if 

( 
administratorId 
== 
Guid #
.# $
Empty$ )
)) *
{ 	
throw 
new !
ArgumentNullException +
(+ ,
$str, I
,I J
nameofK Q
(Q R
administratorIdR a
)a b
)b c
;c d
} 	
if 

( 
	patientId 
== 
Guid 
. 
Empty #
)# $
{ 	
throw 
new !
ArgumentNullException +
(+ ,
$str, C
,C D
nameofE K
(K L
	patientIdL U
)U V
)V W
;W X
} 	
return 
new 
Contract 
( 
administratorId +
,+ ,
	patientId- 6
,6 7
ContractType8 D
.D E
	HalfMonthE N
,N O
	startDateP Y
)Y Z
;Z [
} 
} §<
lC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Contracts\Contract.cs
	namespace 	
Contracting
 
. 
Domain 
. 
	Contracts &
;& '
public 
class 
Contract 
: 
AggregateRoot %
{		 
public

 

Guid

 
AdministratorId

 
{

  !
get

" %
;

% &
set

' *
;

* +
}

, -
public 

Guid 
	PatientId 
{ 
get 
;  
set! $
;$ %
}& '
public 

ContractType 
Type 
{ 
get "
;" #
set$ '
;' (
}) *
public 

ContractStatus 
Status  
{! "
get# &
;& '
set( +
;+ ,
}- .
private 
DateTime 
_creationDate "
;" #
public 

DateTime 
CreationDate  
{ 
get 
=> 
_creationDate 
; 
set 
=> 
_creationDate 
= 
value $
.$ %
Kind% )
==* ,
DateTimeKind- 9
.9 :
Utc: =
?> ?
value@ E
:F G
valueH M
.M N
ToUniversalTimeN ]
(] ^
)^ _
;_ `
} 
private 
DateTime 

_startDate 
;  
public 

DateTime 
	StartDate 
{ 
get 
=> 

_startDate 
; 
set 
=> 

_startDate 
= 
value !
.! "
Kind" &
==' )
DateTimeKind* 6
.6 7
Utc7 :
?; <
value= B
:C D
valueE J
.J K
ToUniversalTimeK Z
(Z [
)[ \
;\ ]
} 
private 
DateTime 
_completedDate #
;# $
public 

DateTime 
? 
CompletedDate "
{ 
get 
=> 
_completedDate 
; 
set 
=> 
_completedDate 
= 
value  %
.% &
HasValue& .
&&/ 1
value2 7
.7 8
Value8 =
.= >
Kind> B
!=C E
DateTimeKindF R
.R S
UtcS V
?W X
valueY ^
.^ _
Value_ d
.d e
ToUniversalTimee t
(t u
)u v
:w x
valuey ~
.~ 
GetValueOrDefault	 ê
(
ê ë
)
ë í
;
í ì
} 
public   

	CostValue   
Cost   
{   
get   
;    
set  ! $
;  $ %
}  & '
private!! 
List!! 
<!! 
DeliveryDay!! 
>!! 
_deliveryDays!! +
;!!+ ,
public"" 

ICollection"" 
<"" 
DeliveryDay"" "
>""" #
DeliveryDays""$ 0
{## 
get$$ 
{%% 	
return&& 
_deliveryDays&&  
;&&  !
}'' 	
}(( 
public** 

Contract** 
(** 
Guid** 
administratorId** (
,**( )
Guid*** .
	patientId**/ 8
,**8 9
ContractType**: F
type**G K
,**K L
DateTime**M U
	startDate**V _
)**_ `
:**a b
base**c g
(**g h
Guid**h l
.**l m
NewGuid**m t
(**t u
)**u v
)**v w
{++ 
AdministratorId,, 
=,, 
administratorId,, )
;,,) *
	PatientId-- 
=-- 
	patientId-- 
;-- 
Type.. 
=.. 
type.. 
;.. 
Status// 
=// 
ContractStatus// 
.//  
Created//  '
;//' (
CreationDate00 
=00 
DateTime00 
.00  
Now00  #
;00# $
	StartDate11 
=11 
	startDate11 
;11 
Cost22 
=22 
CalculateTotalCost22 !
(22! "
type22" &
)22& '
;22' (
_deliveryDays33 
=33 
[33 
]33 
;33 
}44 
public66 
decimal66 
CalculateTotalCost66 "
(66" #
ContractType66# /
type660 4
)664 5
{77 
if88 

(88 
type88 
==88 
ContractType88  
.88  !
	FullMonth88! *
)88* +
{99 	
return:: 
$num:: 
;:: 
};; 	
return<< 
$num<< 
;<< 
}== 
public?? 

void?? 
CreateCalendar?? 
(?? 
List?? #
<??# $
DeliveryDay??$ /
>??/ 0
days??1 5
)??5 6
{@@ 
ifAA 

(AA 
daysAA 
.AA 
CountAA 
==AA 
$numAA 
)AA 
{BB 	
throwCC 
newCC !
ArgumentNullExceptionCC +
(CC+ ,
nameofCC, 2
(CC2 3
daysCC3 7
)CC7 8
,CC8 9
$strCC: O
)CCO P
;CCP Q
}DD 	
_deliveryDaysEE 
=EE 
daysEE 
;EE 
AddDomainEventGG 
(GG 
newGG 
CreateCalendarGG #
(GG# $
IdGG$ &
,GG& '
	PatientIdGG( 1
,GG1 2
	StartDateGG3 <
,GG< =
daysGG> B
[GGB C
^GGC D
$numGGD E
]GGE F
.GGF G
DateGGG K
,GGK L
_deliveryDaysGGM Z
)GGZ [
)GG[ \
;GG\ ]
}HH 
publicJJ 

voidJJ 

InProgressJJ 
(JJ 
)JJ 
{KK 
ifLL 

(LL 
StatusLL 
!=LL 
ContractStatusLL $
.LL$ %
CreatedLL% ,
)LL, -
{MM 	
throwNN 
newNN %
InvalidOperationExceptionNN /
(NN/ 0
$strNN0 ]
)NN] ^
;NN^ _
}OO 	
StatusPP 
=PP 
ContractStatusPP 
.PP  

InProgressPP  *
;PP* +
}QQ 
publicSS 

voidSS 
CompleteSS 
(SS 
)SS 
{TT 
ifUU 

(UU 
StatusUU 
!=UU 
ContractStatusUU $
.UU$ %

InProgressUU% /
)UU/ 0
{VV 	
throwWW 
newWW %
InvalidOperationExceptionWW /
(WW/ 0
$strWW0 m
)WWm n
;WWn o
}XX 	
StatusYY 
=YY 
ContractStatusYY 
.YY  
	CompletedYY  )
;YY) *
CompletedDateZZ 
=ZZ 
DateTimeZZ  
.ZZ  !
NowZZ! $
;ZZ$ %
}[[ 
private]] 
Contract]]	 
(]] 
)]] 
{]] 
}]] 
}^^ ·
ÅC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Administrators\IAdministratorRepository.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Administrators +
;+ ,
public 
	interface $
IAdministratorRepository )
:* +
IRepository, 7
<7 8
Administrator8 E
>E F
{ 
Task 
UpdateAsync	 
( 
Administrator "
administrador# 0
)0 1
;1 2
Task 
DeleteAsync	 
( 
Guid 
id 
) 
; 
}		 √
~C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Administrators\IAdministratorFactory.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Administrators +
;+ ,
public 
	interface !
IAdministratorFactory &
{ 
Administrator 
Create 
( 
string 
administratorName  1
,1 2
string3 9
administratorPhone: L
)L M
;M N
} ´
}C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Administrators\AdministratorFactory.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Administrators +
;+ ,
public 
class  
AdministratorFactory !
:" #!
IAdministratorFactory$ 9
{ 
public 

Administrator 
Create 
(  
string  &
administratorName' 8
,8 9
string: @
administratorPhoneA S
)S T
{ 
if 

( 
string 
. 
IsNullOrWhiteSpace %
(% &
administratorName& 7
)7 8
)8 9
{ 	
throw		 
new		 
ArgumentException		 '
(		' (
$str		( H
,		H I
nameof		J P
(		P Q
administratorName		Q b
)		b c
)		c d
;		d e
}

 	
if 

( 
string 
. 
IsNullOrWhiteSpace %
(% &
administratorPhone& 8
)8 9
)9 :
{ 	
throw 
new 
ArgumentException '
(' (
$str( I
,I J
nameofK Q
(Q R
administratorPhoneR d
)d e
)e f
;f g
} 	
return 
new 
Administrator  
(  !
administratorName! 2
,2 3
administratorPhone4 F
)F G
;G H
} 
} ï

vC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Administrators\Administrator.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Administrators +
;+ ,
public 
class 
Administrator 
: 
AggregateRoot *
{ 
public 

FullNameValue 
Name 
{ 
get  #
;# $
set% (
;( )
}* +
public		 

PhoneNumberValue		 
Phone		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
public 

Administrator 
( 
string 
name  $
,$ %
string& ,
phone- 2
)2 3
:4 5
base6 :
(: ;
Guid; ?
.? @
NewGuid@ G
(G H
)H I
)I J
{ 
Name 
= 
name 
; 
Phone 
= 
phone 
; 
} 
private 
Administrator 
( 
) 
{ 
} 
} ì
rC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Abstractions\IUnitOfWork.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Abstractions )
;) *
public 
	interface 
IUnitOfWork 
{ 
Task 
CommitAsync	 
( 
CancellationToken &
cancellationToken' 8
=9 :
default; B
)B C
;C D
} Ò
rC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Abstractions\IRepository.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Abstractions )
;) *
public 
	interface 
IRepository 
< 
TEntity $
>$ %
where& +
TEntity, 3
:4 5
AggregateRoot6 C
{ 
Task 
< 	
TEntity	 
? 
> 
GetByIdAsync 
(  
Guid  $
id% '
,' (
bool) -
readOnly. 6
=7 8
false9 >
)> ?
;? @
Task 
AddSync	 
( 
TEntity 
entity 
)  
;  !
} Á
mC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Abstractions\Entity.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Abstractions )
;) *
public 
abstract 
class 
Entity 
{ 
public 

Guid 
Id 
{ 
get 
; 
	protected #
set$ '
;' (
}) *
public 

List 
< 
DomainEvent 
> 
_domainEvents *
;* +
public 
ICollection 
< 
DomainEvent 
>  
DomainEvents! -
=>. 0
_domainEvents1 >
;> ?
public		 

Entity		 
(		 
Guid		 
id		 
)		 
{

 
if 

( 
id 
== 
Guid 
. 
Empty 
) 
{ 	
throw 
new 
ArgumentException '
(' (
$str( <
,< =
nameof> D
(D E
idE G
)G H
)H I
;I J
} 	
Id 

= 
id 
; 
_domainEvents 
= 
new 
List  
<  !
DomainEvent! ,
>, -
(- .
). /
;/ 0
} 
public 

void 
AddDomainEvent 
( 
DomainEvent *
domainEvent+ 6
)6 7
{ 
_domainEvents 
. 
Add 
( 
domainEvent %
)% &
;& '
} 
public 

void 
ClearDomainEvents !
(! "
)" #
{ 
_domainEvents 
. 
Clear 
( 
) 
; 
} 
	protected 
Entity 
( 
) 
{ 
_domainEvents 
= 
new 
List  
<  !
DomainEvent! ,
>, -
(- .
). /
;/ 0
}   
}!! ü
rC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Abstractions\DomainEvent.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Abstractions )
;) *
public 
abstract 
record 
DomainEvent "
:# $
INotification% 2
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

DateTime 
	OcurredOn 
{ 
get  #
;# $
set% (
;( )
}* +
public

 

DomainEvent

 
(

 
)

 
{ 
Id 

= 
Guid 
. 
NewGuid 
( 
) 
; 
	OcurredOn 
= 
DateTime 
. 
Now  
;  !
} 
} …
tC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Domain\Abstractions\AggregateRoot.cs
	namespace 	
Contracting
 
. 
Domain 
. 
Abstractions )
;) *
public 
class 
AggregateRoot 
: 
Entity #
{ 
	protected 
AggregateRoot 
( 
Guid  
id! #
)# $
:% &
base' +
(+ ,
id, .
). /
{ 
} 
	protected		 
AggregateRoot		 
(		 
)		 
{

 
} 
} 