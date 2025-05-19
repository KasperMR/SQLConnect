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
if ($result->num_rows === 1) {
    try {
        $user = $result->fetch_assoc();
        if (password_verify($_POST['password'], $user["passwordHash"]))
        {            
            $sql2 = "SELECT users.username, items.itemID, itemName, amount FROM user_item_bank 
             JOIN users on user_item_bank.userID = users.userID
             JOIN items ON user_item_bank.itemID = items.itemID WHERE (users.userID = '$user[userID]')";
            $result2 = $conn->query($sql2);

            $items = array();

            if ($result2->num_rows > 0) {
                while($row = $result2->fetch_assoc()) {
                    $items[] = $row;
                }
                echo json_encode($items);
            } else {
                echo "[]";
            }
        }
    }
    catch (mysqli_sql_exception $e) {        
        echo "error:". $e->getMessage()."\n errorcode: ".$e->getCode();
    }
}


$conn->close();
?>
