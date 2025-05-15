<?php
$host = 'localhost';
$db = 'unityaccess';
$user = 'root';
$pass = ''; // XAMPP default is blank

$conn = new mysqli($host, $user, $pass, $db);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}


$username = $_POST['username'];
$password = password_hash($_POST['password'], PASSWORD_DEFAULT);

$sqlUsernameCheck = "SELECT * FROM users WHERE (username = '$username')";
$result = $conn->query($sqlUsernameCheck);
if ($result->num_rows == 0) {
    $sqlInsert = "INSERT INTO users(username, passwordHash) VALUES ('$username', '$password')";
    try {
        $result = $conn->query($sqlInsert);
        if ($result === TRUE) {
            echo "User created successfully";
        }
        else {
            echo "Error creating user: " . $conn->error;
        }
    }
    catch (mysqli_sql_exception $e) {
        
        echo "error:". $e->getMessage()."\n errorcode: ".$e->getCode();
    }
}
else {
    echo "user already exists";
}
$conn->close();
?>
