<?php
    require '../database.php';

    $login = $_POST['login'];
    $password = $_POST['password'];

    if(isset($login) == false || isset($password) == false){
        echo 'Data struct error';
        exit;
    }

    $repaetChecker = R::findOne('users', 'login = ?', array($login));
    if(isset($repaetChecker)){
        echo 'Login reserved';
        exit;
    }

    $user = R::dispense('users');
    $user -> login = $login;
    $user -> password = $password;


    $avalibleCards = array(1,2,3,4,5,6);
    foreach($avalibleCards as $id){
        $card = R::load('cards', $id);
        $user -> link('cards_users', array('selected' => false)) -> cards = $card;
    }

    R::store($user);

    $selectedIDs = array(1,2,3,4,5);

    $links = $user -> withCondition('cards_users.cards_id IN ('. R::genSlots($selectedIDs) .')', $selectedIDs) -> ownCardsUsers;
    foreach($links as $link){
        $link -> selected = true;
    }

    R::store($user);

    echo 'ok';
?>