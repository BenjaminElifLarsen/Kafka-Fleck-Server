<template>
    <div class="actions">
        <div class="posaction red" @click="$emit('onremove')"><div class="highlight"></div><div class="item">Slet</div></div>
        <div class="posaction yellow" @click="$emit('onreset')"><div class="highlight"></div><div class="item">Nulstil</div></div>
        <div class="posaction green approve" @click="$emit('onbuy')"><div class="highlight"></div><div class="item">Godkend</div></div>
    </div>
</template>

<script>
export default {
}
</script>

<style>
div.actions {
    height:100%;
    position:relative;
}
div.posaction {
  vertical-align:top;
  margin:1%;
  width:98%;
  border:0px;
  margin-top:8px;
  cursor:pointer;
  color:white;
  border-radius: 5px 5px 5px 5px;
}
div.posaction div.item {
    height:10vh;
  font-size: 2vw;
  line-height:2.5vw;
  text-align:center;
}
div.posaction.approve {
    position:absolute;
    bottom:0;
}
div.posaction div.item span {
  vertical-align:center;
}

div.posaction div.highlight {
  height:25px;
  background: #fff4;
  border-radius: 5px 5px 0px 0px;
}

div.posaction.red {
  background: #ff0000;
}
div.posaction.green {
  background: #00cc00;
}
div.posaction.blue {
  background: #0000ff;
}
div.posaction.yellow {
  background: #aaaa00;
}
div.posaction.cyan {
  background: #00aaaa;
}
div.posaction.cyan div.highlight{
  background: #00cccc;
}
</style>