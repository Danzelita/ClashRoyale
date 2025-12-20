<?php 

    require '../database.php';

    $user = R::load('users', 1);
    $card1 = R::load('cards', 1);
    $card2 = R::load('cards', 2);

    $user -> link('cards_users', array('selected' => false)) -> cards = $card1;
    $user -> link('cards_users', array('selected' => false)) -> cards = $card2;

    R::store($user);
?>