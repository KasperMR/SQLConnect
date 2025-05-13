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
$score = $_POST['score'];

$sql = "INSERT INTO players (username, score) VALUES ('$username', $score)";

if ($conn->query($sql) === TRUE) {
    echo "Success";
} else {
    echo "Error: " . $conn->error;
}

$conn->close();
?>
