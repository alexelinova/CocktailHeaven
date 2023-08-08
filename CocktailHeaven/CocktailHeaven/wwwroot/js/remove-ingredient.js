const buttonElement = document.getElementById("removeIngredientBtn");
buttonElement.addEventListener('click', function () {
    const ingredientContainer = document.querySelector("#ingredientsContainer");
    console.log("ingredientContainer:", ingredientContainer);
    const ingredientCount = ingredientContainer.childElementCount;
    console.log("ingredientCount:", ingredientCount);

    if (ingredientCount > 2) {
        const lastElement = ingredientContainer.lastElementChild;
        console.log("lastElement:", lastElement);
        lastElement.remove();
    }
});
