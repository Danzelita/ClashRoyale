<?php 
    require '../database.php';

    $userID = $_POST['userID'];
    $selectedIDsData = $_POST['selectedIDs'];

    if(isset($userID) == false){
        echo 'Data struct error';
        exit;
    }

    if(isset($selectedIDsData) == false){
        echo 'Data struct error';
        exit;
    }

    $selectedIDs = explode('|', $selectedIDsData);
    $selectedIDs = array_map('intval', $selectedIDs);


    $user = R::load('users', $userID);

    $links = $user -> ownCardsUsers;
    foreach($links as $link){
        $link -> selected = in_array($link -> cards_id, $selectedIDs);
    }

    R::store($user);

    echo 'succes';
?>