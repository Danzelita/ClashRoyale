<?php 
    require '../database.php';

    $cardNames = array('Archer', 'Bee', 'Bomb', 'Golem', 'Shell', 'TraningDummy', 'Warrior');

    foreach($cardNames as $name){      
        $card = R::dispense('cards');
        $card -> name = $name;
        R::store($card);
    }
?>