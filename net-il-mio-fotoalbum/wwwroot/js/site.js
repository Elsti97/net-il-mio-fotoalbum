// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const loadPhotos = filter => getPhotos(filter).then(renderPhotos);

const getPhotos = title => axios.get('/api/photos', title ? { params: { title } } : {}).then(res => res.data);

const renderPhotos = photos => {
    const noPhotos = document.querySelector("#no-photos");
    const loader = document.querySelector("#photos-loader");
    const photosTbody = document.querySelector("#photos");
    const photosTable = document.querySelector("#photos-table");
    const photoFilter = document.querySelector("#photos-filter");

    if (photos && photos.length > 0) {
        photosTable.classList.add("d-block");
        photoFilter.classList.add("d-block");
        noPhotos.classList.remove("d-block");
    }
    else { noPhotos.classList.add("d-block"); }

    loader.classList.add("d-none");

    photosTbody.innerHTML = photos.map(photoComponent).join('');
};

const photoComponent = photo => `
    <tr>
        <td>${photo.id}</td>
        <td><a href="/photo/detail/${photo.id}">${photo.title}</a></td>
    </tr>`;


const initFilter = () => {
    const filter = document.querySelector("#photos-filter input");
    filter.addEventListener("input", (e) => loadPhotos(e.target.value))
};