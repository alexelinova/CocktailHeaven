# CocktailHeaven
ASP.NET Core Web App

CocktailHeaven is a simple and user-friendly ASP.NET Core MVC application developed as part of my final project for the ASP.NET Advanced course at SoftUni. The platform is dedicated to inspiring cocktail enthusiasts by providing a wide range of cocktail ideas and recipies.

## Key Features

### For all users:

- **Home page:**  Access to the home page and use the Random Cocktail functionality to get inspired. Check out the top 3 highly rated cocktails.  Browse and view all available cocktails. 


Home Page
![Home Page](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/Homepage.png)

When the user clicks on 'Get a Random Cocktail'
![Random Cocktail](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/RandomCocktail.png)

View All Cocktails - Cocktails are showed in alphabetical order displaying average ratings for the cocktail on top.
![All Cocktails](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/AllCocktails.png)

Login/Register - To use the rest of the functionalities all users have to Login/Register
![Login](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/Loging.png)| ![Register](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/Register.png)

### For logged in/registered users:

- **Review Full Cocktail Details:** Access complete cocktail details, including ingredients and instructions.
- **Collections: Organize cocktails in different collections:** tried, favorites, and wishlist. 
- **Rating and Commenting:** Rate and comment on cocktails that have been tried.
- **Search Functionality:** Use a dedicated Search page to find cocktails by name, ingredient and category.

Cocktail Full Details Page- Clicking the "Show more details" redirects the logged user to the Cocktail Detailed page from where they can also add cocktails to different collections and see existing ratings/comments.
![Full Cocktail Details](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/ShowMore.png)

User Collections - Adding a cocktail to any of the user collections redirects to the relevant user collection and lists all cocktails. From this screen the user can review the cocktail full details or remove the cocktail from the respective collection. The view is identical for both the Whihlist and Tried Cocktails Collections.
![Favourite User Collection](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/CollectionVewWishListAndFavourites.png)

Cocktail Full Details Page - The Add to Collection buttons change to 'In Tried', 'In Favourites', 'In Whishlist' when a cocktail is added to the user collection.
![Full Cocktail Details](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/ShowMoreWhenCocktailIsInCollection.png)

Reviews - Selecting the Reviews options shows all existing users' comments and reviews.
![Reviews Page](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/ShowMoreReviewModal.png)

Tried User Collection - The collection shows all cocktails added to the specific collection and gives the user the option to either Rate a tried cocktail or Edit their rating (if one has already been provided). Additionally, the user can still remove a tried cocktail from this collection. The star rating here represents the specific user's rating.
![Tried Collection](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/TriedUserCollection.png)

![Rate Tried Collection](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/TriedUserCollectionRatepng.png)

![Edit Tried Collection](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/TriedUserCollectionEditRating.png)

Search Cocktails Page - the user can search cocktails by name or by ingredient. They can also review the cocktails in the different categories. By default all cocktails appear on the page.
![Search Cocktails Page](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/SearchPage.png)
### For Cocktail Editor role:
- **Add New Cocktails:** Expand the collection of cocktails and add new recipies.
- **Edit Existing Cocktails:** Modify and update existing cocktail information.
- **Delete Cocktails:** Remove cocktails that are no longer relevant or accurate.

Cocktail Editor Navigation Bar - An additional 'Add' option appears in the Nav Bar available for the specific role only. 
![Cocktail Editor NavBar](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/CocktailEditorNavBar.png)

The 'Add' option redirects the Cocktail Editor to the Add Cocktail page. On successul submission the user is redirected to the Full Cocktail Details Page(Show More Page).
![AddPage](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/CocktailEditorAddCocktailPage.png)

Full Cocktail Details (Cocktail Editor's View) - the user is able to see two additional options - Delete and Edit. The Delete - deletes the cocktail and redirects to the All Cocktails Page. The Edit option lets the user edit the cocktail's details. 
![Cocktail Editor ShowMore Page](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/CocktailEditorShowMorePage.png)

Edit Page - On successfull submission the edit redirects back to the detailed cocktails view so the user can review the updated information.
![Edit Page](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/CocktailEditorEditPage.png)
### For Administrator role

- **User Management:** Review and delete user accounts.
- **Comment Management:** Review and delete inappropriate comments/ratings.
- **Role Management:** Assign or remove roles for users.

Admin Home Page 
![Admin Home Page](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/AdminAreaHomePage.png)

Admin Nav Bar
![Admin Nav Bar](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/AdminNavBar.png)

Ratings Management Page - all comments and ratings are listed chronologically with an option to delete the respective rating/comment.
![Review Ratings and Comments Page](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/AdminAreaRatingsAndComments.png)

User Management Page - all users are listed in alphabetical order with a Delete button on the side. 
![Review and Delete users Page](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/AdminAreReviewAndDeleteUsersPage.png)


Role Assignment Page
![Role Assignment Page](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/AdminAreaAssignandDeleteRoles.png)

## Database Diagram

![DatabaseDiagram](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/DataBaseDiagram.png)

## Tests Code Coverage

![CodeCoverage](https://raw.githubusercontent.com/alexelinova/CocktailHeaven/main/Screenshots/CodeCoverageResharper.png)


## Technologies Used

- ASP.NET Core MVC 6.0
- Entity Framework Core 6.0.14
- Microsoft SQL Server 
- HTML, CSS, JavaScript
- Bootstrap 5, Toastr, Fontawesome
- NUnit 3.13.3