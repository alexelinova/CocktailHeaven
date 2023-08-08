const buttonElement = document.getElementById("removeIngredientBtn");
buttonElement.addEventListener('click', function () {
    const ingredientContainer = document.querySelector("#ingredientsContainer");
    const ingredientCount = ingredientContainer.childElementCount;

    if (ingredientCount > 2) {
        const lastElement = ingredientContainer.lastElementChild;
        lastElement.remove();
    }
});
