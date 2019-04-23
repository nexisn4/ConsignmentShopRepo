# Consignment Shop Demo
## Client provided a description of the program they want.
Create an application that can be used by consignment shops to run their business. You need to know who the individuals/vendors are who have items in the shop and you need to associate these vendors with their items. Each vendor should be assigned a standard commission, but that may need to be changed on per-vendor basis. The application should track how much should be paid to each vendor as well as how much money should be paid to the store.


## Turn that into requirements
1. List of vendors
* List of Items per vendor
* Each vendor should have a default commission rate
* Commissions can change
* Track how much to pay the vendor
* Track how much to pay the store

## Design Doc
Vendor {
	string FirstName
	string LastName
	double Commission
}

Item {
	string Title
	string Description
	decimal Price
	bool Sold
	bool PaymentDistributed
	Vendor Owner
}

Store {
	string Name
	List<Vendor> Vendors
	List<Item> Items
}
