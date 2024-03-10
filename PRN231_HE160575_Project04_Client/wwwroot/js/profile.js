console.log("profile installed !")
const userInfo = {
    userId: -1,
    isActived: true,
    isVerified: true,
    balance: 0,
    roll: -1,
    fullName: 'John Doe',
    birthdate: '1990-01-01',
    phoneNumber: '123-456-7890',
    email: 'john.doe@example.com',
    password: 'Johnpassword',
    address: '123 Main Street, City, Country',
    avatar: 'https://picsum.photos/200/300',
};


const bookingHistory = [
    {
        id: 1,
        name: 'Trọ A',
        bookDate: '2022-01-01',
        endDate: '2022-01-10',
        daysBooked: 10,
        price: 500000,
    },
    {
        id: 2,
        name: 'Trọ B',
        bookDate: '2022-02-15',
        endDate: '2022-02-25',
        daysBooked: 10,
        price: 600000,
    },
];

function handleTabChange(tab) {
    // Ẩn tất cả các tab-content và loại bỏ class active từ tất cả các nút tab
    $('.tab-content').hide();
    $('.btn-content').removeClass('active');

    // Hiển thị tab-content tương ứng với tab được chọn và thêm class active cho nút tab đó
    $(`#${tab}-tab`).show();
    $(`#${tab}-btn`).addClass('active');
}


function handleDetail(bookingId) {
    console.log('Details for booking', bookingId);
    navigate('/detail');
}

$(document).ready(function () {
    $('#profile-btn').click(function () {
        handleTabChange('profile');
    });

    $('#password-btn').click(function () {
        handleTabChange('password');
    });

    $('#bookingHistory-btn').click(function () {
        handleTabChange('bookingHistory');
    });

    

});


function encodeImageFileAsURL(element) {
    var file = element.files[0];
    var reader = new FileReader();
    reader.onloadend = function () {
        console.log('Encoded image base64 string:', reader.result);
        $('#Avatar').attr('src', reader.result); 
        $('#AvatarInput').val(reader.result); 
    }
    reader.readAsDataURL(file);
}
