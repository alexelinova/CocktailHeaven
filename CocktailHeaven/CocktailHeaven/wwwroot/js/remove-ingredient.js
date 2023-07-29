const buttonElement = document.getElementById("removeIngredientBtn");
buttonElement.addEventListener('click', function () {
    const ingredientContainer = document.querySelector("#ingredientsContainer");
    const lastElement = ingredientContainer.lastElementChild;

    lastElement.remove();
});
