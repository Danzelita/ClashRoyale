<?php 
    require '../database.php';

    $userID = $_POST['userID'];

    if(isset($userID) == false){
        echo 'Data struct error';
        exit;
    }

    $user = R::load('users', $userID);
    $allCards = $user -> sharedCards;

    $avalibleCards = [];
    foreach($allCards as $card){
        $avalibleCards[] = $card -> export();
    }
    $avalibleCardsJson = json_encode($avalibleCards);

    $selectedCardBeans = $user -> withCondition('cards_users.selected = ?', array(true)) -> sharedCards;
    $selectedIDs = json_encode(array_column($selectedCardBeans, 'id'));

    echo '{"avalibleCards":'. $avalibleCardsJson .', "selectedIDs":'. $selectedIDs .'}';
?>