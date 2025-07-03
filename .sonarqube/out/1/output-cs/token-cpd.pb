�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Patients\OutboxMessageHandlers\PublishProductCreated.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Patients" *
.* +!
OutboxMessageHandlers+ @
;@ A
public 
class !
PublishProductCreated "
(" #
IExternalPublisher# 5!
integrationBusService6 K
)K L
:M N 
INotificationHandlerO c
<c d
OutboxMessaged q
<q r
PatientCreated	r �
>
� �
>
� �
{		 
public

 
async

 
Task

 
Handle

 
(

 
OutboxMessage

 '
<

' (
PatientCreated

( 6
>

6 7
notification

8 D
,

D E
CancellationToken

F W
canellationToken

X h
)

h i
{ !
PatientCreatedMessage 
message 
=  !
new" %
(% &
notification 
. 
Content 
. 
	PatientId !
,! "
notification 
. 
Content 
. 
Name 
, 
notification 
. 
Content 
. 
Phone 
, 
notification 
. 
CorrelationId 
, 
$str 
) 
; 
await !
integrationBusService 
. 
PublishAsync *
(* +
message+ 2
)2 3
;3 4
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Patients\OutboxMessageHandlers\PatientCreatedMessage.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Patients" *
.* +!
OutboxMessageHandlers+ @
;@ A
public 
record !
PatientCreatedMessage #
(# $
Guid$ (
	PatientId) 2
,2 3
string4 :
Name; ?
,? @
stringA G
PhoneH M
,M N
stringO U
?U V
CorrelationIdW d
=e f
nullg k
,k l
stringm s
?s t
Sourceu {
=| }
null	~ �
)
� �
:
� � 
IntegrationMessage
� �
(
� �
CorrelationId
� �
,
� �
Source
� �
)
� �
;
� ��
~C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Patients\GetPatients\PatientDto.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Patients" *
.* +
GetPatients+ 6
;6 7
public 
class 

PatientDto 
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

string 
PatientName 
{ 
get  #
;# $
set% (
;( )
}* +
public 

string 
PatientPhone 
{  
get! $
;$ %
set& )
;) *
}+ ,
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Patients\GetPatients\GetPatientsQuery.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Patients" *
.* +
GetPatients+ 6
;6 7
public 
record 
GetPatientsQuery 
( 
string %

SearchTerm& 0
)0 1
:2 3
IRequest4 <
<< =
IEnumerable= H
<H I

PatientDtoI S
>S T
>T U
;U V�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Patients\GetPatientById\GetPatientByIdQuery.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Patients" *
.* +
GetPatientById+ 9
;9 :
public 
record 
GetPatientByIdQuery !
(! "
Guid" &
	PatientId' 0
)0 1
:2 3
IRequest4 <
<< =
Result= C
<C D

PatientDtoD N
>N O
>O P
;P Q�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Patients\DomainEventHandlers\SaveInOutboxWhenPatienCreated.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Patients" *
.* +
DomainEventHandlers+ >
;> ?
public		 
class		 )
SaveInOutboxWhenPatienCreated		 *
(		* +
IOutboxService		+ 9
<		9 :
DomainEvent		: E
>		E F
OutboxService		G T
,		T U
IUnitOfWork		V a

UnitOfWork		b l
)		l m
:		n o!
INotificationHandler			p �
<
		� �
PatientCreated
		� �
>
		� �
{

 
public 
async 
Task 
Handle 
( 
PatientCreated (
domainEvent) 4
,4 5
CancellationToken6 G
cancellationTokenH Y
)Y Z
{ 
OutboxMessage 
< 
DomainEvent 
> 
outboxMessage *
=+ ,
new- 0
(0 1
domainEvent1 <
)< =
;= >
await 
OutboxService 
. 
AddAsync 
( 
outboxMessage ,
), -
;- .
await 

UnitOfWork 
. 
CommitAsync 
( 
cancellationToken 0
)0 1
;1 2
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Patients\CreatePatient\CreatePatientHandler.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Patients" *
.* +
CreatePatient+ 8
;8 9
public 
class  
CreatePatientHandler !
(! "
IPatienteFactory" 2
PatientFactory3 A
,A B
IPatientRepositoryC U
PatientRepositoryV g
,g h
IUnitOfWorki t

UnitOfWorku 
)	 �
:
� �
IRequestHandler
� �
<
� �"
CreatePatientCommand
� �
,
� �
Result
� �
<
� �
Guid
� �
>
� �
>
� �
{		 
public

 

async

 
Task

 
<

 
Result

 
<

 
Guid

 !
>

! "
>

" #
Handle

$ *
(

* + 
CreatePatientCommand

+ ?
request

@ G
,

G H
CancellationToken

I Z
cancellationToken

[ l
)

l m
{ 
var 
patient 
= 
PatientFactory $
.$ %
Create% +
(+ ,
request, 3
.3 4
PatientName4 ?
,? @
requestA H
.H I
PatientPhoneI U
)U V
;V W
await 
PatientRepository 
.  
AddSync  '
(' (
patient( /
)/ 0
;0 1
await 

UnitOfWork 
. 
CommitAsync $
($ %
cancellationToken% 6
)6 7
;7 8
return 
patient 
. 
Id 
; 
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Patients\CreatePatient\CreatePatientCommand.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Patients" *
.* +
CreatePatient+ 8
;8 9
public 
record  
CreatePatientCommand "
(" #
string# )
PatientName* 5
,5 6
string7 =
PatientPhone> J
)J K
:L M
IRequestN V
<V W
ResultW ]
<] ^
Guid^ b
>b c
>c d
;d e�
rC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\DependencyInjection.cs
	namespace 	
Contracting
 
. 
Application !
;! "
public

 
static

 
class

 
DependencyInjection

 '
{ 
public 

static 
IServiceCollection $
AddApplication% 3
(3 4
this4 8
IServiceCollection9 K
servicesL T
)T U
{ 
services 

.
 

AddMediatR 
( 
config 
=> 
{ 
config 	
.	 
(
RegisterServicesFromAssembly
 &
(& '
Assembly' /
./ 0 
GetExecutingAssembly0 D
(D E
)E F
)F G
;G H
config 	
.	 

AddOpenBehavior
 
( 
typeof  
(  !*
RequestLoggingPipelineBehavior! ?
<? @
,@ A
>A B
)B C
)C D
;D E
} 
) 
; 
services 

.
 
AddSingleton 
< !
IAdministratorFactory -
,- . 
AdministratorFactory/ C
>C D
(D E
)E F
;F G
services 
. 
AddSingleton 
< 
IPatienteFactory .
,. /
PatientFactory0 >
>> ?
(? @
)@ A
;A B
services 
. 
AddSingleton 
< 
IContractFactory .
,. /
ContractFactory0 ?
>? @
(@ A
)A B
;B C
return 
services 
; 
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\UpdateDeliveryDays\UpdateDeliveryDaysHandler.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
UpdateDeliveryDays, >
;> ?
public 
class %
UpdateDeliveryDaysHandler &
(& '
IContractRepository' :
contractRepository; M
,M N
IUnitOfWorkO Z

unitOfWork[ e
)e f
:g h
IRequestHandleri x
<x y&
UpdateDeliveryDaysCommand	y �
,
� �
Result
� �
<
� �
Guid
� �
>
� �
>
� �
{		 
public

 

async

 
Task

 
<

 
Result

 
<

 
Guid

 !
>

! "
>

" #
Handle

$ *
(

* +%
UpdateDeliveryDaysCommand

+ D
request

E L
,

L M
CancellationToken

N _
cancellationToken

` q
)

q r
{ 
var 
contract 
= 
await 
contractRepository /
./ 0
GetByIdAsync0 <
(< =
request= D
.D E

ContractIdE O
)O P
;P Q
if 
( 
contract 
== 
null 
) 
{ 
throw 
new	 %
InvalidOperationException &
(& '
$str' ;
); <
;< =
} 
var 
daysToUpdate 
= 
contract 
. 
DeliveryDays *
. 
Where 
( 
d 
=> 
d 
. 
Date 
. 
Date #
>=$ &
request' .
.. /
	FirstDate/ 8
.8 9
Date9 =
&&> @
dA B
.B C
DateC G
.G H
DateH L
<=M O
requestP W
.W X
LastDateX `
.` a
Datea e
)e f
. 
ToList 
( 
) 
; 
if 

( 
daysToUpdate 
. 
Count 
== !
$num" #
)# $
{ 	
throw 
new %
InvalidOperationException /
(/ 0
$str0 `
)` a
;a b
} 
foreach 
( 
var 
day 
in 
daysToUpdate (
)( )
{ 	
day 
. 
Update 
( 
request 
. 
Street %
,% &
request' .
.. /
Number/ 5
)5 6
;6 7
} 	
await   

unitOfWork   
.   
CommitAsync   $
(  $ %
cancellationToken  % 6
)  6 7
;  7 8
return"" 
Result"" 
."" 
Success"" 
("" 
contract"" &
.""& '
Id""' )
)"") *
;""* +
}## 
}$$ �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\UpdateDeliveryDays\UpdateDeliveryDaysCommand.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
UpdateDeliveryDays, >
;> ?
public 
record %
UpdateDeliveryDaysCommand '
(' (
Guid( ,

ContractId- 7
,7 8
DateTime9 A
	FirstDateB K
,K L
DateTimeM U
LastDateV ^
,^ _
string` f
Streetg m
,m n
into r
Numbers y
)y z
:{ |
IRequest	} �
<
� �
Result
� �
<
� �
Guid
� �
>
� �
>
� �
;
� ��
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\UpdateDeliveryDayById\UpdateDeliveyDayByIdCommand.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,!
UpdateDeliveryDayById, A
;A B
public 
record '
UpdateDeliveyDayByIdCommand )
() *
Guid* .

ContractId/ 9
,9 :
Guid; ?
DeliveryDayId@ M
,M N
stringO U
StreetV \
,\ ]
int^ a
Numberb h
)h i
:i j
IRequestk s
<s t
Resultt z
<z {
Guid{ 
>	 �
>
� �
;
� ��
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\UpdateDeliveryDayById\UpdateDeliveryDayByIdHandler.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,!
UpdateDeliveryDayById, A
;A B
public 
class (
UpdateDeliveryDayByIdHandler )
() *
IContractRepository* =
contractRepository> P
,P Q
IUnitOfWorkR ]

unitOfWork^ h
)h i
:j k
IRequestHandlerl {
<{ |(
UpdateDeliveyDayByIdCommand	| �
,
� �
Result
� �
<
� �
Guid
� �
>
� �
>
� �
{		 
public

 

async

 
Task

 
<

 
Result

 
<

 
Guid

 !
>

! "
>

" #
Handle

$ *
(

* +'
UpdateDeliveyDayByIdCommand

+ F
request

G N
,

N O
CancellationToken

P a
cancellationToken

b s
)

s t
{ 
var 
contract 
= 
await 
contractRepository /
./ 0
GetByIdAsync0 <
(< =
request= D
.D E

ContractIdE O
)O P
;P Q
if 

( 
contract 
== 
null 
) 
{ 	
throw 
new %
InvalidOperationException /
(/ 0
$str0 D
)D E
;E F
} 	
var 
day 
= 
contract 
. 
DeliveryDays '
.' (
FirstOrDefault( 6
(6 7
d7 8
=>9 ;
d< =
.= >
Id> @
==A C
requestD K
.K L
DeliveryDayIdL Y
)Y Z
;Z [
if 

( 
day 
== 
null 
) 
{ 	
throw 
new %
InvalidOperationException /
(/ 0
$str0 H
)H I
;I J
} 	
day 
. 
Update 
( 
request 
. 
Street !
,! "
request# *
.* +
Number+ 1
)1 2
;2 3
await 

unitOfWork 
. 
CommitAsync $
($ %
cancellationToken% 6
)6 7
;7 8
return 
Result 
. 
Success 
( 
contract &
.& '
Id' )
)) *
;* +
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\OutboxMessageHandlers\PublishDeliveryDayUpdated.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,!
OutboxMessageHandlers, A
;A B
public		 
class		 %
PublishDeliveryDayUpdated		 &
(		& '
IExternalPublisher		' 9!
integrationBusService		: O
)		O P
:		Q R 
INotificationHandler		S g
<		g h
OutboxMessage		h u
<		u v
DeliveryDayUpdated			v �
>
		� �
>
		� �
{

 
public 
async 
Task 
Handle 
( 
OutboxMessage '
<' (
DeliveryDayUpdated( :
>: ;
notification< H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ %
DeliveryDayUpdatedMessage 
message #
=$ %
new& )
() *
notification 
. 
Content 
. 

ContractId "
," #
notification 
. 
Content 
. 
DeliveryDayId %
,% &
notification 
. 
Content 
. 
Street 
, 
notification 
. 
Content 
. 
Number 
, 
notification 
. 
CorrelationId 
, 
$str 
) 
; 
await !
integrationBusService 
. 
PublishAsync *
(* +
message+ 2
)2 3
;3 4
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\OutboxMessageHandlers\PublishDeliveryDayDeleted.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,!
OutboxMessageHandlers, A
;A B
public 
class %
PublishDeliveryDayDeleted &
(& '
IExternalPublisher' 9!
integrationBusService: O
)O P
:Q R 
INotificationHandlerS g
<g h
OutboxMessageh u
<u v
DeliveryDayDeleted	v �
>
� �
>
� �
{		 
public

 
async

 
Task

 
Handle

 
(

 
OutboxMessage

 '
<

' (
DeliveryDayDeleted

( :
>

: ;
notification

< H
,

H I
CancellationToken

J [
cancellationToken

\ m
)

m n
{ %
DeliveryDayDeletedMessage 
message #
=$ %
new& )
() *
notification 
. 
Content 
. 

ContractId "
," #
notification 
. 
Content 
. 
DeliveryDayId %
,% &
notification 
. 
CorrelationId 
, 
$str 
) 
; 
await !
integrationBusService 
. 
PublishAsync *
(* +
message+ 2
)2 3
;3 4
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\OutboxMessageHandlers\PublishCalendarCreated.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,!
OutboxMessageHandlers, A
;A B
public 
class "
PublishCalendarCreated #
(# $
IExternalPublisher$ 6!
integrationBusService7 L
)L M
:N O 
INotificationHandlerP d
<d e
OutboxMessagee r
<r s
CreateCalendar	s �
>
� �
>
� �
{		 
public

 
async

 
Task

 
Handle

 
(

 
OutboxMessage

 '
<

' (
CreateCalendar

( 6
>

6 7
notification

8 D
,

D E
CancellationToken

F W
cancellationToken

X i
)

i j
{ "
CalendarCreatedMessage 
message  
=! "
new# &
(& '
notification 
. 
Content 
. 

ContractId "
," #
notification 
. 
Content 
. 
	PatientId !
,! "
notification 
. 
Content 
. 
	StartDate !
,! "
notification 
. 
Content 
. 
EndDate 
,  
notification 
. 
Content 
. 
DeliveryDays $
,$ %
notification 
. 
CorrelationId 
, 
$str 
) 
; 
await !
integrationBusService 
. 
PublishAsync *
(* +
message+ 2
)2 3
;3 4
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\OutboxMessageHandlers\DeliveryDayUpdatedMessage.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,!
OutboxMessageHandlers, A
;A B
public 
record %
DeliveryDayUpdatedMessage '
(' (
Guid( ,

ContractId- 7
,7 8
Guid8 <
DeliveryDayId= J
,J K
stringL R
StreetS Y
,Y Z
int[ ^
Number_ e
,e f
stringg m
?m n
CorrelationIdo |
=} ~
null	 �
,
� �
string
� �
?
� �
Source
� �
=
� �
null
� �
)
� �
:
� � 
IntegrationMessage
� �
(
� �
CorrelationId
� �
,
� �
Source
� �
)
� �
;
� ��
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\OutboxMessageHandlers\DeliveryDayDeletedMessage.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,!
OutboxMessageHandlers, A
;A B
public 
record %
DeliveryDayDeletedMessage '
(' (
Guid( ,

ContractId- 7
,7 8
Guid9 =
DeliveryDayId> K
,K L
stringM S
?S T
CorrelationIdU b
=c d
nulle i
,i j
stringk q
?q r
Sources y
=z {
null	| �
)
� �
:
� � 
IntegrationMessage
� �
(
� �
CorrelationId
� �
,
� �
Source
� �
)
� �
;
� ��	
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\OutboxMessageHandlers\CalendarCreatedMessage.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,!
OutboxMessageHandlers, A
;A B
public 
record "
CalendarCreatedMessage $
($ %
Guid% )

ContractId* 4
,4 5
Guid6 :
	PatiendId; D
,D E
DateTimeF N
	StartTimeO X
,X Y
DateTimeZ b
EndDatec j
,j k
Listl p
<p q
DeliveryDayq |
>| }
DeliveryDays	~ �
,
� �
string
� �
?
� �
CorrelationId
� �
=
� �
null
� �
,
� �
string
� �
?
� �
Source
� �
=
� �
null
� �
)
� �
:
� � 
IntegrationMessage
� �
(
� �
CorrelationId
� �
,
� �
Source
� �
)
� �
;
� ��
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\InProgressContract\InProgressContractHandler.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
InProgressContract, >
;> ?
public 
class %
InProgressContractHandler &
(& '
IContractRepository' :
contractRepository; M
,M N
IUnitOfWorkO Z

UnitOfWork[ e
)e f
:g h
IRequestHandleri x
<x y&
InProgressContractCommand	y �
,
� �
bool
� �
>
� �
{ 
public		 
async		 
Task		 
<		 
bool		 
>		 
Handle		 
(		  %
InProgressContractCommand		  9
request		: A
,		A B
CancellationToken		C T
cancellationToken		U f
)		f g
{

 
var 
contract 
= 
await 
contractRepository )
.) *
GetByIdAsync* 6
(6 7
request7 >
.> ?

ContractId? I
)I J
;J K
if 
( 
contract 
== 
null 
) 
{ 
throw 
new	 %
InvalidOperationException &
(& '
$str' ;
); <
;< =
} 
contract 

.
 

InProgress 
( 
) 
; 
await 

UnitOfWork 
. 
CommitAsync 
( 
cancellationToken 0
)0 1
;1 2
return 
true	 
; 
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\InProgressContract\InProgressContractCommand.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
InProgressContract, >
;> ?
public 
record %
InProgressContractCommand '
(' (
Guid( ,

ContractId- 7
)7 8
:9 :
IRequest; C
<C D
boolD H
>H I
;I J�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\GetDeliveryDay\GetDeliveryDayQuery.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
GetDeliveryDay, :
;: ;
public 
record 
GetDeliveryDayQuery !
(! "
Guid" &
DeliveryDayId' 4
)4 5
:6 7
IRequest8 @
<@ A
ResultA G
<G H
DeliveryDayDtoH V
>V W
>W X
;X Y�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\GetContracts\GetContractsQuery.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
GetContracts, 8
;8 9
public 
record 
GetContractsQuery 
(  
string  &

SearchTerm' 1
)1 2
:3 4
IRequest5 =
<= >
IEnumerable> I
<I J
ContractDtoJ U
>U V
>V W
;W X�	
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\GetContracts\DeliveryDayDto.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
GetContracts, 8
;8 9
public 
class 
DeliveryDayDto 
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

Guid 

ContractId 
{ 
get  
;  !
set" %
;% &
}' (
public 

required 
string 
Street !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 

int 
Number 
{ 
get 
; 
set !
;! "
}# $
public		 

DateTime		 
DateTime		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
}

 �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\GetContractById\GetContractByIdQuery.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
GetContractById, ;
;; <
public 
record  
GetContractByIdQuery "
(" #
Guid# '

ContractId( 2
)2 3
:4 5
IRequest6 >
<> ?
Result? E
<E F
ContractDtoF Q
>Q R
>R S
;S T�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\GetContractById\ContractDto.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
GetContractById, ;
;; <
public 
class 
ContractDto 
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

Guid 
AdministratorId 
{  !
get" %
;% &
set' *
;* +
}, -
public		 

Guid		 
	PatientId		 
{		 
get		 
;		  
set		! $
;		$ %
}		& '
public

 

required

 
string

 
Type

 
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

required 
string 
Status !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 

DateTime 
CreationDate  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 

DateTime 
	StartDate 
{ 
get  #
;# $
set% (
;( )
}* +
public 

DateTime 
CompleteDate  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 

decimal 
	CostValue 
{ 
get "
;" #
set$ '
;' (
}) *
public 

required 
IEnumerable 
<  
DeliveryDayDto  .
>. /
DeliveryDays0 <
{= >
get? B
;B C
setD G
;G H
}I J
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\DomainEventHandlers\SaveInOutboxWhenDeliveryDayUpdated.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
DomainEventHandlers, ?
;? @
public		 
class		 .
"SaveInOutboxWhenDeliveryDayUpdated		 /
(		/ 0
IOutboxService		0 >
<		> ?
DomainEvent		? J
>		J K
OutboxService		L Y
,		Y Z
IUnitOfWork		[ f

UnitOfWork		g q
)		q r
:		s t!
INotificationHandler			u �
<
		� � 
DeliveryDayUpdated
		� �
>
		� �
{

 
public 
async 
Task 
Handle 
( 
DeliveryDayUpdated ,
notification- 9
,9 :
CancellationToken; L
cancellationTokenM ^
)^ _
{ 
OutboxMessage 
< 
DomainEvent 
> 
outboxMessage *
=+ ,
new- 0
(0 1
notification1 =
)= >
;> ?
await 
OutboxService 
. 
AddAsync 
( 
outboxMessage ,
), -
;- .
await 

UnitOfWork 
. 
CommitAsync 
( 
cancellationToken 0
)0 1
;1 2
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\DomainEventHandlers\SaveInOutboxWhenDeliveryDayDeleted.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
DomainEventHandlers, ?
;? @
public		 
class		 .
"SaveInOutboxWhenDeliveryDayDeleted		 /
(		/ 0
IOutboxService		0 >
<		> ?
DomainEvent		? J
>		J K
OutboxService		L Y
,		Y Z
IUnitOfWork		[ f

UnitOfWork		g q
)		q r
:		s t!
INotificationHandler			u �
<
		� � 
DeliveryDayDeleted
		� �
>
		� �
{

 
public 
async 
Task 
Handle 
( 
DeliveryDayDeleted ,
notification- 9
,9 :
CancellationToken; L
cancellationTokenM ^
)^ _
{ 
OutboxMessage 
< 
DomainEvent 
> 
outboxMessage *
=+ ,
new- 0
(0 1
notification1 =
)= >
;> ?
await 
OutboxService 
. 
AddAsync 
( 
outboxMessage ,
), -
;- .
await 

UnitOfWork 
. 
CommitAsync 
( 
cancellationToken 0
)0 1
;1 2
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\DomainEventHandlers\SaveInOutboxWhenCalendarCreated.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
DomainEventHandlers, ?
;? @
public		 
class		 +
SaveInOutboxWhenCalendarCreated		 ,
(		, -
IOutboxService		- ;
<		; <
DomainEvent		< G
>		G H
OutboxService		I V
,		V W
IUnitOfWork		X c

UnitOfWork		d n
)		n o
:		p q!
INotificationHandler			r �
<
		� �
CreateCalendar
		� �
>
		� �
{

 
public 
async 
Task 
Handle 
( 
CreateCalendar (
domainEvent) 4
,4 5
CancellationToken6 G
cancellationTokenH Y
)Y Z
{ 
OutboxMessage 
< 
DomainEvent 
> 
outboxMessage *
=+ ,
new- 0
(0 1
domainEvent1 <
)< =
;= >
await 
OutboxService 
. 
AddAsync 
( 
outboxMessage ,
), -
;- .
await 

UnitOfWork 
. 
CommitAsync 
( 
cancellationToken 0
)0 1
;1 2
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\DeleteDeliveryDay\DeleteDeliveryDayCommand.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
DeleteDeliveryDay, =
;= >
public 
record $
DeleteDeliveryDayCommand &
(& '
Guid' +

ContractId, 6
,6 7
Guid8 <
DeliveryDayId= J
)J K
:L M
IRequestN V
<V W
ResultW ]
<] ^
Guid^ b
>b c
>c d
;d e�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\DeleteDeliveryDay\DeleteDeliveryDayHandler.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
DeleteDeliveryDay, =
;= >
public 
class $
DeleteDeliveryDayHandler %
(% &
IContractRepository& 9
contractRepository: L
,L M
IUnitOfWorkN Y

unitOfWorkZ d
)d e
:f g
IRequestHandlerh w
<w x%
DeleteDeliveryDayCommand	x �
,
� �
Result
� �
<
� �
Guid
� �
>
� �
>
� �
{		 
public

 

async

 
Task

 
<

 
Result

 
<

 
Guid

 !
>

! "
>

" #
Handle

$ *
(

* +$
DeleteDeliveryDayCommand

+ C
request

D K
,

K L
CancellationToken

M ^
cancellationToken

_ p
)

p q
{ 
var 
contract 
= 
await 
contractRepository /
./ 0
GetByIdAsync0 <
(< =
request= D
.D E

ContractIdE O
)O P
;P Q
if 

( 
contract 
== 
null 
) 
{ 	
throw 
new	 %
InvalidOperationException &
(& '
$str' ;
); <
;< =
} 
var 
dayToRemove 
= 
contract "
." #
DeliveryDays# /
./ 0
FirstOrDefault0 >
(> ?
d? @
=>A C
dD E
.E F
IdF H
==I K
requestL S
.S T
DeliveryDayIdT a
)a b
;b c
if 

( 
dayToRemove 
== 
null 
)  
{ 	
throw 
new	 %
InvalidOperationException &
(& '
$str' >
)> ?
;? @
} 
dayToRemove 
. 
Delete 
( 
) 
; 
contract 
. 
DeliveryDays 
. 
Remove $
($ %
dayToRemove% 0
)0 1
;1 2
await 

unitOfWork 
. 
CommitAsync $
($ %
cancellationToken% 6
)6 7
;7 8
return 
Result 
. 
Success 
( 
request %
.% &

ContractId& 0
)0 1
;1 2
} 
} �"
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\CreateContract\CreateContractHandler.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
CreateContract, :
;: ;
public		 
class		 !
CreateContractHandler		 "
(		" #
IContractFactory		# 3
ContractFactory		4 C
,		C D
IContractRepository		E X
ContractRepository		Y k
,		k l
IUnitOfWork		m x

UnitOfWork			y �
)
		� �
:
		� �
IRequestHandler
		� �
<
		� �#
CreateContractCommand
		� �
,
		� �
Result
		� �
<
		� �
Guid
		� �
>
		� �
>
		� �
{

 
public 

async 
Task 
< 
Result 
< 
Guid !
>! "
>" #
Handle$ *
(* +!
CreateContractCommand+ @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 
var 
contract 
= 
request 
. 
Type #
switch$ *
{ 
$str 
=> 
ContractFactory !
.! "#
CreateHalfMonthContract" 9
(9 :
request: A
.A B
AdministratorIdB Q
,Q R
requestS Z
.Z [
	PatientId[ d
,d e
requestf m
.m n
	StartDaten w
)w x
,x y
$str 
=> 
ContractFactory !
.! "#
CreateFullMonthContract" 9
(9 :
request: A
.A B
AdministratorIdB Q
,Q R
requestS Z
.Z [
	PatientId[ d
,d e
requestf m
.m n
	StartDaten w
)w x
,x y
_ 
=> 
throw 
new #
NotImplementedException )
() *
)* +
} 
; 
List 
< 
DeliveryDay 
> 
deliveryDays &
=' (
[) *
]* +
;+ ,
foreach 
( 
var 
days 
in 
request $
.$ %
Days% )
)) *
{ 	
int 
span 
; 
if 
( 
request 
. 
Type 
== 
$str  +
)+ ,
{ 
span 
= 
$num 
; 
} 
else 
{ 
span 
= 
$num 
; 
} 
for   
(   
int   
i   
=   
$num   
;   
i   
<=    
span  ! %
;  % &
i  ' (
++  ( *
)  * +
{!! 
DeliveryDay"" 
d"" 
="" 
new""  #
(""# $
contract""$ ,
."", -
Id""- /
,""/ 0
days""1 5
.""5 6
Start""6 ;
.""; <
AddDays""< C
(""C D
i""D E
)""E F
,""F G
days""H L
.""L M
Street""M S
,""S T
days""U Y
.""Y Z
Number""Z `
)""` a
;""a b
deliveryDays## 
.## 
Add##  
(##  !
d##! "
)##" #
;### $
}$$ 
}%% 	
contract&& 
.&& 
CreateCalendar&& 
(&&  
deliveryDays&&  ,
)&&, -
;&&- .
await(( 
ContractRepository((  
.((  !
AddSync((! (
(((( )
contract(() 1
)((1 2
;((2 3
await)) 

UnitOfWork)) 
.)) 
CommitAsync)) $
())$ %
cancellationToken))% 6
)))6 7
;))7 8
return** 
contract** 
.** 
Id** 
;** 
}++ 
},, �	
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\CreateContract\CreateContractCommand.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
CreateContract, :
;: ;
public 
record !
CreateContractCommand #
(# $
Guid$ (
AdministratorId) 8
,8 9
Guid: >
	PatientId? H
,H I
stringJ P
TypeQ U
,U V
DateTimeW _
	StartDate` i
,i j
ICollectionk v
<v w&
CreateDeliveryDaysCommand	w �
>
� �
Days
� �
)
� �
:
� �
IRequest
� �
<
� �
Result
� �
<
� �
Guid
� �
>
� �
>
� �
;
� �
public 
record %
CreateDeliveryDaysCommand '
(' (
DateTime( 0
Start1 6
,6 7
string8 >
Street? E
,E F
intG J
NumberK Q
)Q R
;R S�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\CompleteContract\CompleteContractHandler.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
CompleteContract, <
;< =
public 
class #
CompleteContractHandler $
($ %
IContractRepository% 8
ContractRepository9 K
,K L
IUnitOfWorkM X

UnitOfWorkY c
)c d
:e f
IRequestHandlerg v
<v w$
CompleteContractCommand	w �
,
� �
bool
� �
>
� �
{ 
public		 
async		 
Task		 
<		 
bool		 
>		 
Handle		 
(		  #
CompleteContractCommand		  7
request		8 ?
,		? @
CancellationToken		A R
cancellationToken		S d
)		d e
{

 
var 
contract 
= 
await 
ContractRepository )
.) *
GetByIdAsync* 6
(6 7
request7 >
.> ?

ContractId? I
)I J
;J K
if 
( 
contract 
== 
null 
) 
{ 
throw 
new	 %
InvalidOperationException &
(& '
$str' ;
); <
;< =
} 
contract 

.
 
Complete 
( 
) 
; 
await 

UnitOfWork 
. 
CommitAsync 
( 
cancellationToken 0
)0 1
;1 2
return 
true	 
; 
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Contracts\CompleteContract\CompleteContractCommand.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Contracts" +
.+ ,
CompleteContract, <
;< =
public 
record #
CompleteContractCommand %
(% &
Guid& *

ContractId+ 5
)5 6
:7 8
IRequest9 A
<A B
boolB F
>F G
;G H�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Behaviors\RequestLoggingPipelineBehavior.cs
	namespace 	
Contracting
 
. 
Application !
.! "
	Behaviors" +
;+ ,
public 
sealed 
class *
RequestLoggingPipelineBehavior 2
<2 3
TRequest3 ;
,; <
	TResponse= F
>F G
(G H
ILogger		 
<		 	*
RequestLoggingPipelineBehavior			 '
<		' (
TRequest		( 0
,		0 1
	TResponse		2 ;
>		; <
>		< =
logger		> D
)		D E
:

 
IPipelineBehavior

 
<

 
TRequest

 
,

 
	TResponse

 (
>

( )
where 
TRequest 
: 
class 
where 
	TResponse 
: 
Result 
{ 
public 
async 
Task 
< 
	TResponse 
> 
Handle $
($ %
TRequest 

request 
, "
RequestHandlerDelegate 
< 
	TResponse "
>" #
next$ (
,( )
CancellationToken 
cancellationToken %
)% &
{ 
string 
requestName	 
= 
typeof 
( 
TRequest &
)& '
.' (
Name( ,
;, -
logger 
. 	
LogInformation	 
( 
$str :
,: ;
requestName< G
)G H
;H I
	TResponse 
result 
= 
await 
next 
(  
)  !
;! "
if 
( 
result 
. 
	IsSuccess 
) 
{ 
logger 	
.	 

LogInformation
 
( 
$str :
,: ;
requestName< G
)G H
;H I
} 
else 
{ 
LogError 
( 
result 
. 
Error 
, 
requestName %
)% &
;& '
}   
return"" 
result""	 
;"" 
}$$ 
private&& 
void&&	 
LogError&& 
(&& 
Error&& 
error&& "
,&&" #
string&&$ *
requestName&&+ 6
)&&6 7
{'' 
using(( 
((( 	

LogContext((	 
.(( 
PushProperty((  
(((  !
$str((! (
,((( )
error((* /
,((/ 0
true((1 5
)((5 6
)((6 7
{)) 
logger** 	
.**	 

LogError**
 
(** 
$str** ?
,**? @
requestName**A L
)**L M
;**M N
}++ 
},, 
}-- �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Administrators\GetAdministrators\GetAdministratorsQuery.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Administrators" 0
.0 1
GetAdministrators1 B
;B C
public 
record "
GetAdministratorsQuery $
($ %
string% +

SearchTerm, 6
)6 7
:8 9
IRequest: B
<B C
IEnumerableC N
<N O
AdministratorDtoO _
>_ `
>` a
;a b�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Administrators\GetAdministrators\AdministratorDto.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Administrators" 0
.0 1
GetAdministrators1 B
;B C
public 
class 
AdministratorDto 
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

required 
string 
AdministratorName ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 

required 
string 
AdministratorPhone -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Administrators\GetAdministratorById\GetAdministratorByIdQuery.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Administrators" 0
.0 1 
GetAdministratorById1 E
;E F
public 
record %
GetAdministratorByIdQuery '
(' (
Guid( ,
AdministratorId- <
)< =
:= >
IRequest? G
<G H
ResultH N
<N O
AdministratorDtoO _
>_ `
>` a
;a b�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Administrators\CreateAdministrator\CreateAdministratorHandler.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Administrators" 0
.0 1
CreateAdministrator1 D
;D E
public 
class &
CreateAdministratorHandler '
(' (!
IAdministratorFactory( = 
AdministratorFactory> R
,R S$
IAdministratorRepositoryT l$
AdministratorRepository	m �
,
� �
IUnitOfWork
� �

UnitOfWork
� �
)
� �
:
� �
IRequestHandler
� �
<
� �(
CreateAdministratorCommand
� �
,
� �
Result
� �
<
� �
Guid
� �
>
� �
>
� �
{		 
public

 

async

 
Task

 
<

 
Result

 
<

 
Guid

 !
>

! "
>

" #
Handle

$ *
(

* +&
CreateAdministratorCommand

+ E
request

F M
,

M N
CancellationToken

O `
cancellationToken

a r
)

r s
{ 
var 
administrator 
=  
AdministratorFactory 0
.0 1
Create1 7
(7 8
request8 ?
.? @
AdministratorName@ Q
,Q R
requestS Z
.Z [
AdministratorPhone[ m
)m n
;n o
await #
AdministratorRepository %
.% &
AddSync& -
(- .
administrator. ;
); <
;< =
await 

UnitOfWork 
. 
CommitAsync $
($ %
cancellationToken% 6
)6 7
;7 8
return 
administrator 
. 
Id 
;  
} 
} �
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Administrators\CreateAdministrator\CreateAdministratorCommand.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Administrators" 0
.0 1
CreateAdministrator1 D
;D E
public 
record &
CreateAdministratorCommand (
(( )
string) /
AdministratorName0 A
,A B
stringC I
AdministratorPhoneJ \
)\ ]
:^ _
IRequest` h
<h i
Resulti o
<o p
Guidp t
>t u
>u v
;v w�
�C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.Application\Abstractions\ICorrelationIdProvider.cs
	namespace 	
Contracting
 
. 
Application !
.! "
Abstractions" .
;. /
public 
	interface "
ICorrelationIdProvider '
{ 
string 
GetCorrelationId 
( 
) 
; 
void 
SetCorrelationId 
( 
string 
correlationId +
)+ ,
;, -
} 