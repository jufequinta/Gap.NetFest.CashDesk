declare @contProviders int
declare @lenProviders int = 1

declare @sizeBrandChocolates int = 3000
declare @brandPerProvider int = 0

declare @tableNameChocolates table (id int identity(1,1),nameChocolate varchar(50))
declare @tableTypeChocolates table (id int identity(1,1),nameChocolate varchar(50))

insert into @tableNameChocolates(nameChocolate)
values ('Milkies')

insert into @tableNameChocolates(nameChocolate)
values ('Ribbles')

insert into @tableNameChocolates(nameChocolate)
values ('Diggies')

insert into @tableNameChocolates(nameChocolate)
values ('Milkies blank')

insert into @tableNameChocolates(nameChocolate)
values ('Rolers')

insert into @tableNameChocolates(nameChocolate)
values ('Blasters')

insert into @tableNameChocolates(nameChocolate)
values ('Googlies')

insert into @tableNameChocolates(nameChocolate)
values ('Comets')

insert into @tableNameChocolates(nameChocolate)
values ('Sweeties')

insert into @tableNameChocolates(nameChocolate)
values ('Pummels')

insert into @tableNameChocolates(nameChocolate)
values ('Rainbows')

insert into @tableNameChocolates(nameChocolate)
values ('Merries')

insert into @tableNameChocolates(nameChocolate)
values ('Cerealice')

insert into @tableNameChocolates(nameChocolate)
values ('Nutters')

insert into @tableNameChocolates(nameChocolate)
values ('Gobbles')

insert into @tableNameChocolates(nameChocolate)
values ('Jimbles')

insert into @tableNameChocolates(nameChocolate)
values ('Crawlies')

insert into @tableNameChocolates(nameChocolate)
values ('Baboos')

insert into @tableNameChocolates(nameChocolate)
values ('Dumdums')

insert into @tableNameChocolates(nameChocolate)
values ('Cheeries')


-- Types chocolate
insert into @tableTypeChocolates(nameChocolate)
values ('60% Cocoa')

insert into @tableTypeChocolates(nameChocolate)
values ('Chocolate filling')

insert into @tableTypeChocolates(nameChocolate)
values ('75% Cocoa')

insert into @tableTypeChocolates(nameChocolate)
values ('Crunch')

insert into @tableTypeChocolates(nameChocolate)
values ('Peanut Chocolate')

select @lenProviders = Max(id), @contProviders = min(id) from dbo.Providers
select @brandPerProvider = cast((@sizeBrandChocolates/(select count(*) from dbo.Providers)) as int)

while (@contProviders <= @lenProviders) begin
	print ('Id provider '+ cast(@contProviders as varchar(2)))
	declare @contBrand int  = 1
	while(@contBrand <= @brandPerProvider) begin
		declare @rand int = 0 
		declare @chocolateName varchar(50)
		declare @exist bit = 0
		while(@exist = 0) begin
			SELECT @rand = ROUND(((20 - 1) * RAND() + 1), 0)
			select @chocolateName = nameChocolate from @tableNameChocolates
			where id = @rand

			SELECT @rand = ROUND(((20 - 1) * RAND() + 1), 0)
		
			select @chocolateName = @chocolateName +' '+ nameChocolate from @tableNameChocolates
			where id = @rand

			SELECT @rand = ROUND(((5 - 1) * RAND() + 1), 0)

			select @chocolateName = @chocolateName +' '+ nameChocolate from @tableTypeChocolates
			where id = @rand

			declare @len int  = 0
			select @len = count(*) from dbo.Chocolates_Brands
			where [name] =  @chocolateName and id_provider = @contProviders

			if(@len = 0) begin
				set @exist = 1
			end

		end
		print ('Brand #: ' + cast(@contBrand as varchar(3)) + ' '+ @chocolateName)

		-- Create Brand per Brand
		declare @price money = 1
		SELECT @price = cast (ROUND(((5 - 1) * RAND() + 1), 0) as money)

		insert into dbo.Chocolates_Brands (name,descripcion,id_provider,price)
		values(@chocolateName,@chocolateName,@contProviders,@price)

		set @contBrand = @contBrand + 1
	end

	set @contProviders = @contProviders + 1
end