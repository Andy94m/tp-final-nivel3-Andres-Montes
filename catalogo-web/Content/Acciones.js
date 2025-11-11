document.addEventListener("DOMContentLoaded", function () {
    const temaGuardado = localStorage.getItem("tema") || "dark";
    document.documentElement.setAttribute("data-bs-theme", temaGuardado);

    const iconoSol = document.querySelector(".fa-sun");
    const iconoLuna = document.querySelector(".fa-moon");

    iconoSol.style.display = temaGuardado === "dark" ? "inline-block" : "none";
    iconoLuna.style.display = temaGuardado === "light" ? "inline-block" : "none";

    iconoSol.addEventListener("click", () => {
        document.documentElement.setAttribute("data-bs-theme", "light");
        localStorage.setItem("tema", "light");
        iconoSol.style.display = "none";
        iconoLuna.style.display = "inline-block";
    });

    iconoLuna.addEventListener("click", () => {
        document.documentElement.setAttribute("data-bs-theme", "dark");
        localStorage.setItem("tema", "dark");
        iconoSol.style.display = "inline-block";
        iconoLuna.style.display = "none";
    });
});