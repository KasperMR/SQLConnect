<?php
$host = 'localhost';
$db = 'unityaccess';
$user = 'root';
$pass = ''; // XAMPP default is blank

$conn = new mysqli($host, $user, $pass, $db);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username, score FROM players ORDER BY score DESC LIMIT 10";
$result = $conn->query($sql);

$players = array();

if ($result->num_rows > 0) {
    while($row = $result->fetch_assoc()) {
        $players[] = $row;
    }
    echo json_encode($players);
} else {
    echo "[]";
}

$conn->close();
?>
