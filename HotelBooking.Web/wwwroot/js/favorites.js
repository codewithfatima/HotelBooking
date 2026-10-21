
function getFavourite() {
    try {
        const saved = JSON.parse(
            localStorage.getItem("favoriteHotels") || "[]"
        );

        return Array.isArray(saved) ? saved.map(String) : [];
    } catch {
        return [];
    }
}

function toggleFavorite(button, event) {
    if (event) {
        event.preventDefault();
        event.stopPropagation();
    }

    const hotelId = String(button.dataset.hotelId);
    if (!hotelId || hotelId === "undefined") return;

    let favorites = getFavourite();

    if (favorites.includes(hotelId)) {
        favorites = favorites.filter(id => id !== hotelId);
    } else {
        favorites.push(hotelId);
    }

    localStorage.setItem("favoriteHotels", JSON.stringify(favorites));

    updateFavoriteButton(button, favorites.includes(hotelId));
}

function updateFavoriteButton(button, isFavorite) {
    const icon = button.querySelector("i");
    if (!icon) return;

    button.classList.toggle("active", isFavorite);

    icon.classList.toggle("bi-heart", !isFavorite);
    icon.classList.toggle("bi-heart-fill", isFavorite);

    button.setAttribute(
        "aria-label",
        isFavorite ? "Remove from favorites" : "Add to favorites"
    );
}

function initializeFavorites() {
    const favorites = getFavourite();

    document.querySelectorAll(".favorite-btn").forEach(button => {
        updateFavoriteButton(
            button,
            favorites.includes(String(button.dataset.hotelId))
        );
    });
}

if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", initializeFavorites);
} else {
    initializeFavorites();
}
